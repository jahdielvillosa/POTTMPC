using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobsV1.Models
{
    public class AppUser
    {
        public string UserName { get; set; }
    }

    public class InvItemsModified
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string ImgPath { get; set; }
        public List<InvItemCategory> CategoryList { get; set; }
    }

    public class DailyUpdate
    {
        public int Id { get; set; }
        public string StatusCategory { get; set; }
        public DateTime dtTaken { get; set; }
        public int refId { get; set; }          
        public string Details { get; set; }
    }

    #region Item schedule classes
    public class getItemSchedReturn
    {
        public List<ItemSchedule> ItemSched { get; set; }
        public List<DayLabel> dLabel { get; set; }
    }

    public class ItemSchedule
    {
        [Key]
        public int ItemId { get; set; }
        public Models.InvItem Item { get; set; }
        public List<DayStatus> dayStatus { get; set; }
    }
    public class DayStatus
    {
        [Key]
        public int Day { get; set; }
        public DateTime Date { get; set; }
        public int status { get; set; }
        public List<JobServices> svc { get; set; }
    }

    public class cItemSchedule
    {
        [Key]
        public int ItemId { get; set; }
        public int? JobId { get; set; }
        public int? ServiceId { get; set; }
        public DateTime? DtStart { get; set; }
        public DateTime? DtEnd { get; set; }
    }

    public class DayLabel
    {
        public int iDay { get; set; }
        public string sDayName { get; set; }
        public string sDayNo { get; set; }
    }

    #endregion

    public class DBClasses
    {
        JobDBContainer db = new JobDBContainer();

        public IList<AppUser> getUsers()
        {
            var data = db.Database.SqlQuery<AppUser>("Select UserName from AspNetUsers");
            return data.ToList();
        }


        public getItemSchedReturn ItemSchedules()
        {
            #region get itemJobs
            string SqlStr = @"
select  a.Id ItemId, c.JobMainId, c.Id ServiceId, c.Particulars, c.DtStart, c.DtEnd from 
InvItems a
left outer join JobServiceItems b on b.InvItemId = a.Id 
left outer join JobServices c on b.JobServicesId = c.Id
left outer join JobMains d on c.JobMainId = d.Id
where d.JobStatusId < 4
;";
            List<cItemSchedule> itemJobs = db.Database.SqlQuery<cItemSchedule>(SqlStr).ToList();

            //cItemSchedule
            #endregion

            int NoOfDays = 20;
            DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            List<ItemSchedule> ItemSched = new List<ItemSchedule>();

            var InvItems = db.InvItems.ToList().OrderBy(s=>s.OrderNo);
            var ItemId = db.InvItems.Select(s => s.Id).ToList();


            foreach (var tmpItem in InvItems)
            {
                ItemSchedule ItemTmp = new ItemSchedule();

                ItemTmp.ItemId = tmpItem.Id;
                ItemTmp.Item = tmpItem;
                ItemTmp.dayStatus = new List<DayStatus>();

                Console.WriteLine(ItemTmp.Item.Description);

                var JobServiceList = itemJobs.Where(d => d.ItemId == tmpItem.Id);
                for (int i = 0; i <= NoOfDays; i++)
                {
                    DayStatus dsTmp = new DayStatus();
                    dsTmp.Date = dtStart.AddDays(i);
                    dsTmp.Day = i + 1;
                    dsTmp.status = 0;

                    //Check if your Messages collection exists
                    if (dsTmp.svc == null)
                    {
                        //It's null - create it
                        dsTmp.svc = new List<JobServices>();
                    }


                    foreach (var jsTmp in JobServiceList)
                    {
                        int istart = dsTmp.Date.CompareTo(jsTmp.DtStart);
                        int iend = dsTmp.Date.CompareTo(jsTmp.DtEnd);

                        if (istart >= 0 && iend <= 0)
                        {
                            dsTmp.status += 1;
                            JobServices js = db.JobServices.Where(j => j.Id == jsTmp.ServiceId).FirstOrDefault();
                            dsTmp.svc.Add( js );  
                        }

                        
                    }

                    ItemTmp.dayStatus.Add(dsTmp);
                }


                ItemSched.Add(ItemTmp);
            }

            //Day Label
            List<DayLabel> dLabel = new List<DayLabel>();
            for (int i = 0; i <= NoOfDays; i++)
            {
                DateTime dtDay = dtStart.AddDays(i);

                DayLabel dsTmp = new DayLabel();
                dsTmp.iDay = i + 1;
                dsTmp.sDayName = dtDay.ToString("ddd");
                dsTmp.sDayNo = dtDay.ToString("dd");

                dLabel.Add(dsTmp);
            }
            
            getItemSchedReturn dReturn = new getItemSchedReturn();
            dReturn.dLabel = dLabel;
            dReturn.ItemSched = ItemSched;

            return dReturn;
        }

        public void addNotification(string Module, string Desc) {

            db.JobNotificationRequests.Add(new JobNotificationRequest {
                ReqDt = DateTime.Parse(DateTime.Now.ToString("MMM dd yyyy HH:mm:ss")),
                ServiceId = 4   //SMS service Id
            
            });
            db.SaveChanges();


            db.JobServices.Add(new JobServices {
                Id = 0,
                SupplierId = 1,
                SupplierItemId= 1,
                JobMainId = 4,
                ServicesId = 1,
                Remarks = Module + " - " + Desc
            });
            db.SaveChanges();
        }


        public void addTestNotification(int transId,string webhookId)
        {

            db.JobNotificationRequests.Add(new JobNotificationRequest
            {
                ReqDt = DateTime.Parse(DateTime.Now.ToString("MMM dd yyyy HH:mm:ss")),
                ServiceId = transId,   //SMS service Id
                RefId = webhookId.ToString()
            });
            db.SaveChanges();
            
        }

        //record encoder info 
        public void addEncoderRecord(string reftable, string refid, string user, string action) {

            DateTime today = DateTime.Now;
            today = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(today, TimeZoneInfo.Local.Id, "Singapore Standard Time");


            db.JobTrails.Add(new JobTrail {
                RefTable = reftable,
                RefId = refid,
                user = user,
                Action = action,
                dtTrail = today
            });

            db.SaveChanges();
        }
    }
}