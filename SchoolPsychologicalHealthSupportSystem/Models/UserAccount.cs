﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SchoolPsychologicalHealthSupportSystem.Models;

public partial class UserAccount
{
    public int UserAccountId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string EmployeeCode { get; set; }

    public int RoleId { get; set; }

    public string RequestCode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string ApplicationCode { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string ModifiedBy { get; set; }

    public bool IsActive { get; set; }
}