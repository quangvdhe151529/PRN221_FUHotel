using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ControllerDAO
{
    public class RoomInforDAO
    {
        private static RoomInforDAO instance = null;
        public static readonly object instanceLock = new object();
        private RoomInforDAO() { }
        public static RoomInforDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomInforDAO();
                    }
                    return instance;
                }
            }
        }

        public List<RoomInformation> GetAll() { 
            List<RoomInformation> ltRoonInfor = new List<RoomInformation>();
            try
            {
                using (var context = new PRN212_SU24_AS1Context())
                {
                    ltRoonInfor = context.RoomInformations.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ltRoonInfor;

        }

    }
}
