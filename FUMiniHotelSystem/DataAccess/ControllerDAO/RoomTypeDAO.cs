using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ControllerDAO
{

    public class RoomTypeDAO
    {
        private static RoomTypeDAO instance = null;
        public static readonly object instanceLock = new object();

        public static RoomTypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomTypeDAO();
                    }
                    return instance;
                }
            }
        }
        public List<RoomType> GetAll()
        {
            List<RoomType> roleTypes = new List<RoomType>();
            try
            {
                using (var context = new PRN212_SU24_AS1Context())
                {
                    roleTypes = context.RoomTypes.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roleTypes;
        }
    }
}
