﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lending.Models
{
    public class tblApplicant
    {
        [Key]
        public Int32 Id { get; set; }
        public String ApplicantCode { get; set; }
        public String FullName { get; set; }
        public String BirthDate { get; set; }
        public Int32 CivilStatusId { get; set; }
        public String CivilStatus { get; set; }
        public String CityAddress { get; set; }
        public String ProvinceAddress { get; set; }
        public Int32 ResidenceTypeId { get; set; }
        public String ResidenceType { get; set; }
        public Decimal LengthOfStay { get; set; }
        public Int32 CreatedByUserId { get; set; }
        public String CreatedByUser { get; set; }
        public String CreatedDateTime { get; set; }
        public Int32 UpdatedByUserId { get; set; }
        public String UpdatedByUser { get; set; }
        public String UpdatedDateTime { get; set; }
    }
}