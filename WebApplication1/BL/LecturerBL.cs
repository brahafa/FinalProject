using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicker.Classes;
using Clicker.DAL;

namespace Clicker.BL
{
    public class LecturerBL
    {
         public static LecturerDAL lecturerDAL;
        //List<classes.Lecturer> accessoriesList;
        public LecturerBL()
        {
            lecturerDAL = new LecturerDAL();
        }
        public void UpdateLecturer(int id,String Name, String image, String password, String degree)
        {
            lecturerDAL.UpdateLecturer(id, Name,  image,  password, degree);
        }

        public void AddNewLecturer(int Id, String Name, String email, String image, String password, String degree)
        {
            lecturerDAL.AddNewLecturer( Id,  Name,  email, image,password, degree);
        }
         public void deleteLecturerById(int Id)
         {
             lecturerDAL.deleteLecturerById(Id);
         }
         public int maxIdLecturer()
         {
             return lecturerDAL.maxIdLecturer();
         }
         public List<Lecturer> getLecturerByPassword(String password, String email)
         {
             return lecturerDAL.getLecturerByPassword(password,  email);
         }
         public List<Lecturer> getLecturerByEmail(String email)
         {
             return lecturerDAL.getLecturerByEmail(email);
         }

    }
}