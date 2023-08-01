using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace Member_Details.Pages.Members
{
    public class IndexModel : PageModel
    {
        public List<MemberInfo> ListMembers = new List<MemberInfo>();
        public void OnGet()
        {
            try
            {


                String connectionString = "Data Source=OSAMA126\\SQLEXPRESS;Initial Catalog=\"Member list\";Integrated Security=True";

                Console.WriteLine("Connected 1");
                using (SqlConnection connection= new SqlConnection(connectionString))
                {
                    connection.Open();
                    String SQL = "SELECT * FROM Members ";

                    Console.WriteLine("Connected  2");



                    using (SqlCommand command = new SqlCommand(SQL, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            Console.WriteLine("Connected  3");

                            while (reader.Read())
                            {
                                MemberInfo memberInfo = new MemberInfo();
                                memberInfo.id =  reader.GetInt32(0).ToString();
                                memberInfo.name = reader.GetString(1);
                                memberInfo.email = reader.GetString(2);
                                memberInfo.phone = reader.GetString(3);
                                memberInfo.dob = reader.GetDateTime(4).ToString("dd-MM-yyyy");
                                memberInfo.gender = reader.GetString(5);

                                ListMembers.Add(memberInfo);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
                return;
            }
        }
    }


    public class MemberInfo
    {
        public string? id;
        public string? name;
        public string? email;
        public string? phone;
        public string? dob;
        public string? gender;


    }

}
