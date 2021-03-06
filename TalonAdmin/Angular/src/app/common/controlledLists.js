﻿angular
.module('talon.common')
.service('controlledLists', ['entityManagerFactory', '$q', '$sessionStorage', '$localStorage',
    function (entityManagerFactory, $q, $sessionStorage, $localStorage) {
        var entityManager = entityManagerFactory.entityManager();
        var adminEntityManager = entityManagerFactory.adminEntityManager();

        return {
            reportTypes: function () {
                var deferred = $q.defer();
                deferred.resolve([
                    { id: 1, name: 'Vendor Financial Report' },
                    { id: 2, name: 'Distribution Report' },
                    { id: 3, name: 'Program Closure Report' }
                ]);
                return deferred.promise;
            },
            smsBackends: function () {
                var deferred = $q.defer();
                deferred.resolve([
                    { id: 0, name: 'RescueSMS' },
                    { id: 1, name: 'TurboSMS' },
                    { id: 2, name: 'Clickatell' }
                ]);
                return deferred.promise;
            },
            sexes: function () {
                var deferred = $q.defer();
                deferred.resolve([
                    { id: 0, name: 'Male' },
                    { id: 1, name: 'Female' }
                ]);
                return deferred.promise;
            },
            validAfterOffsetTypes: function () {
                var deferred = $q.defer();
                deferred.resolve([
                    { id: 1, name: 'Voucher is distributed' },
                    { id: 2, name: 'Day(s)' },
                    { id: 3, name: 'Week(s)' }
                ]);
                return deferred.promise;
            },
            distributionMechanisms: function () {
                var deferred = $q.defer();
                deferred.resolve([
                    { id: 1, name: 'SMS' },
                    { id: 2, name: 'NFC' },
                    { id: 3, name: 'QR-Code' }
                ]);
                return deferred.promise;
            },
            locations: function () {
                var deferred = $q.defer();
                entityManagerFactory.entityQuery("Locations")
                   .using(entityManager)
                   .noTracking()
                   .execute()
                   .then(function (res) {
                       deferred.resolve(res.results);
                   });

                return deferred.promise;
            },
            voucherTypes: function () {
                var deferred = $q.defer();
                entityManagerFactory.entityQuery("VoucherTypes")
                   .using(entityManager)
                   .noTracking()
                   .execute()
                   .then(function (res) {
                       deferred.resolve(res.results);
                   });

                return deferred.promise;
            },
            beneficiaryGroups: function () {
                var deferred = $q.defer();
                entityManagerFactory.entityQuery("BeneficiaryGroups")
               .using(entityManager)
               .noTracking()
               .execute()
               .then(function (res) {
                   deferred.resolve(res.results);
               });

                return deferred.promise;
            },
            vendorTypes: function () {
                var deferred = $q.defer();
                entityManagerFactory.entityQuery("VendorTypes")
               .using(entityManager)
               .noTracking()
               .execute()
               .then(function (res) {
                   deferred.resolve(res.results);
               });

                return deferred.promise;
            },
            vendors: function () {
                var deferred = $q.defer();
                entityManagerFactory.entityQuery("Vendors")
               .using(entityManager)
               .noTracking()
               .execute()
               .then(function (res) {
                   deferred.resolve(res.results);
               });

                return deferred.promise;
            },
            distributions: function () {
                var deferred = $q.defer();
                entityManagerFactory.entityQuery("Distributions")
               .using(entityManager)
               .noTracking()
               .execute()
               .then(function (res) {
                   deferred.resolve(res.results);
               });

                return deferred.promise;
            },
            programs: function () {
                var deferred = $q.defer();
                entityManagerFactory.entityQuery("Programs")
               .using(entityManager)
               .noTracking()
               .execute()
               .then(function (res) {
                   deferred.resolve(res.results);
               });

                return deferred.promise;
            },
            organizations: function () {
                var deferred = $q.defer();
                if ($sessionStorage.organizations) {
                    deferred.resolve($sessionStorage.organizations);
                } else {
                    entityManagerFactory.entityQuery("Organizations")
                   .using(adminEntityManager)
                   .noTracking()
                   .execute()
                   .then(function (res) {
                       $sessionStorage.organizations = res.results;
                       deferred.resolve(res.results);
                   });
                }

                return deferred.promise;
            },
            countries: function () {
                var deferred = $q.defer();

                entityManagerFactory.entityQuery("Countries")
                .using(adminEntityManager)
                .noTracking()
                .execute()
                   .then(function (res) {
                       deferred.resolve(res.results);
                   });

                return deferred.promise;
            },
            roles: function () {
                var deferred = $q.defer();
                entityManagerFactory.entityQuery("Roles")
                   .using(adminEntityManager)
                   .noTracking()
                   .execute()
                   .then(function (res) {
                       deferred.resolve(res.results);
                   });
                return deferred.promise;
            },
            menuCategories: function () {
                var deferred = $q.defer();
                entityManagerFactory.entityQuery("MenuCategories")
                   .using(adminEntityManager)
                   .noTracking()
                   .execute()
                   .then(function (res) {
                       deferred.resolve(res.results);
                   });
                return deferred.promise;
            }

        };
    }]);
