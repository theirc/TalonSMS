﻿using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using TalonAdmin.Extensions;
using TalonAdmin.Attributes;

namespace TalonAdmin.Controllers.Breeze
{
    [BreezeController, AuthorizeTenant, EnableBreezeQuery(MaxExpansionDepth = 5)]
    public class EVMController : ApiController
    {

        readonly EFContextProvider<Models.Vouchers.Context> _contextProvider =
            new EFContextProvider<Models.Vouchers.Context>();

        // ~/breeze/todos/Metadata 
        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.Beneficiary> Beneficiaries()
        {

            return _contextProvider.Context
                .Beneficiaries
                .FilterCountry(this)
                .FilterOrganization(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.BeneficiaryGroup> BeneficiaryGroups()
        {
            return _contextProvider.Context.BeneficiaryGroups
                .FilterCountry(this)
                .FilterOrganization(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.Program> Programs()
        {
            return _contextProvider.Context.Programs
                .FilterCountry(this)
                .FilterOrganization(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.Distribution> Distributions()
        {
            return _contextProvider.Context.Distributions
                .FilterCountry(this)
                .FilterOrganization(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.Beneficiary> DistributionBeneficiaries(int distributionId)
        {
            return _contextProvider.Context.VoucherTransactionRecords
                .FilterCountry(this)
                .Where(v=> v.Type == 1)
                .Where(v=>v.Voucher.DistributionId == distributionId)
                .Select(v=>v.Beneficiary)
                .Distinct();
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.ExportedReport> ExportedReports()
        {
            return _contextProvider.Context.ExportedReports
                .FilterCountry(this)
                .FilterOrganization(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.DistributionLog> DistributionLogs()
        {
            return _contextProvider.Context.DistributionLogs
                .FilterCountry(this)
                .FilterOrganization(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.DistributionVoucherCategory> DistributionVoucherCategories()
        {
            return _contextProvider.Context.DistributionVoucherCategories
                .FilterCountry(this)
                .FilterOrganization(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.Location> Locations()
        {
            return _contextProvider.Context.Locations
                .FilterCountry(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.MessageLog> MessageLogs()
        {
            return _contextProvider.Context.MessageLogs;
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.AuditLogItem> AuditLogItems()
        {
            return _contextProvider.Context.AuditLogItems;
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.Vendor> Vendors()
        {
            return _contextProvider.Context.Vendors
                .FilterCountry(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.VendorDevice> VendorDevices()
        {
            return _contextProvider.Context.VendorDevices
                .FilterCountry(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.VendorSalesPerson> VendorSalesPersons()
        {
            return _contextProvider.Context.VendorSalesPersons
                .FilterCountry(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.VendorType> VendorTypes()
        {
            return _contextProvider.Context.VendorTypes
                .FilterCountry(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.Voucher> Vouchers()
        {
            return _contextProvider.Context.Vouchers
                .FilterCountry(this)
                .FilterOrganization(this); 
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.VoucherType> VoucherTypes()
        {
            return _contextProvider.Context.VoucherTypes
                .FilterCountry(this)
                .FilterOrganization(this); 
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.VoucherTransactionRecord> VoucherTransactionRecords()
        {
            return _contextProvider.Context.VoucherTransactionRecords
                .FilterCountry(this)
                .FilterOrganization(this);
        }
        /// <summary>
        /// Filtered Voucher Transactions limited to just vouchers issued.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IQueryable<Models.Vouchers.VoucherTransactionRecord> IssuedVoucherTransactionRecords()
        {
            return _contextProvider.Context.VoucherTransactionRecords
                .Where(v=> v.Type == 1)
                .FilterCountry(this)
                .FilterOrganization(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.TransactionLogItem> TransactionLogItems()
        {
            return _contextProvider.Context.TransactionLogItems
                .FilterCountry(this)
                .FilterOrganization(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.CardLoad> CardLoads()
        {
            return _contextProvider.Context.CardLoads
                .FilterCountry(this)
                .FilterOrganization(this);
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.Vendor> DistributionVendors(int distributionId)
        {
            return _contextProvider.Context.VoucherTransactionRecords
                .Where(v => v.Voucher.DistributionId == distributionId && v.VendorId != null)
                .Select(v => v.Vendor)
                .Distinct();
        }

        [HttpGet]
        public IQueryable<Models.Vouchers.Vendor> ProgramVendors(int programId)
        {
            return _contextProvider.Context.VoucherTransactionRecords
                .Where(v => v.Voucher.Distribution.ProgramId == programId && v.VendorId != null)
                .Select(v => v.Vendor)
                .Distinct();
        }


        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }
    }
}