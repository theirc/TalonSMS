﻿'use strict';
app.factory('controlledListService', ['breeze', 'backendService', 'adminBackendService', '$q', '$sessionStorage', '$localStorage',
    function (breeze, backendService, adminBackendService, $q, $sessionStorage, $localStorage) {
        return {
            getLocations: function () {
                var deferred = $q.defer();
                new breeze.EntityQuery("Locations")
                   .using(backendService)
                   .noTracking()
                   .execute()
                   .then(function (res) {
                       deferred.resolve(res.results);
                   });

                return deferred.promise;
            },
            getVoucherTypes: function () {
                var deferred = $q.defer();
                new breeze.EntityQuery("VoucherTypes")
                   .using(backendService)
                   .noTracking()
                   .execute()
                   .then(function (res) {
                       deferred.resolve(res.results);
                   });

                return deferred.promise;
            },
            getBeneficiaryGroups: function () {
                var deferred = $q.defer();
                new breeze.EntityQuery("BeneficiaryGroups")
               .using(backendService)
               .noTracking()
               .execute()
               .then(function (res) {
                   deferred.resolve(res.results);
               });

                return deferred.promise;
            },
            getVendorTypes: function () {
                var deferred = $q.defer();
                new breeze.EntityQuery("VendorTypes")
               .using(backendService)
               .noTracking()
               .execute()
               .then(function (res) {
                   deferred.resolve(res.results);
               });

                return deferred.promise;
            },
            getOrganizations: function () {
                var deferred = $q.defer();
                if ($sessionStorage.organizations) {
                    deferred.resolve($sessionStorage.organizations);
                } else {
                    new breeze.EntityQuery("Organizations")
                   .using(adminBackendService)
                   .noTracking()
                   .execute()
                   .then(function (res) {
                       $sessionStorage.organizations = res.results;
                       deferred.resolve(res.results);
                   });
                }

                return deferred.promise;
            },
            getCountries: function () {
                var deferred = $q.defer();
                if ($sessionStorage.countries) {
                    deferred.resolve($sessionStorage.countries);
                } else {
                    new breeze.EntityQuery("Countries")
                   .using(adminBackendService)
                   .noTracking()
                   .execute()
                   .then(function (res) {
                       $sessionStorage.countries = res.results;
                       deferred.resolve(res.results);
                   });
                }
                return deferred.promise;
            },
            getRoles: function () {
                var deferred = $q.defer();
                if ($localStorage.roles) {
                    deferred.resolve($localStorage.roles);
                } else {
                    new breeze.EntityQuery("Roles")
                   .using(adminBackendService)
                   .noTracking()
                   .execute()
                   .then(function (res) {
                       $localStorage.roles = res.results;
                       deferred.resolve(res.results);
                   });
                }
                return deferred.promise;
            }

        };
    }]);

app.factory('organizations', ['controlledListService', function (controlledListService) {
    return controlledListService.getOrganizations();
}])
app.factory('countries', ['controlledListService', function (controlledListService) {
    return controlledListService.getCountries();
}])
app.factory('roles', ['controlledListService', function (controlledListService) {
    return controlledListService.getRoles();
}])
app.factory('locations', ['controlledListService', function (controlledListService) {
    return controlledListService.getLocations();
}])