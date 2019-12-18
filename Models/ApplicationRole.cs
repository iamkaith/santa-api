using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Assignment2.Data {
    public class ApplicationRole : IdentityRole {

        public static string ADMIN = "Admin";
        public static string CHILD = "Child"; 
        Dictionary<string, string> roles; 

       public ApplicationRole() : base() { } 
       
       public ApplicationRole(string roleName) 
       :base(roleName) {} 
    }
}