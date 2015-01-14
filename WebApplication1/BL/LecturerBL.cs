using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.classes;
using WebApplication1.DAL;

namespace WebApplication1.BL
{
    public class LecturerBL
    {
         public static LecturerDAL lecturerDAL;
        //List<classes.Lecturer> accessoriesList;
        public LecturerBL()
        {
            lecturerDAL = new LecturerDAL();
        }
         public void AddNewLecturer(int Id, String Name, String email, String image, String password)
        {
            lecturerDAL.AddNewLecturer( Id,  Name,  email, image,password);
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