//adds the resource to umbraco.resources module:
angular.module('umbraco.resources').factory('gtmCategoryResource',
    function ($q, $http) {
        //the factory object returned
        return {
            //this calls the ApiController we setup earlier
            getAll: function () {
                return $http.get("backoffice/Gibe/LinkPickerApi/GetAllCategories");
            },
            saveCategory: function(gtmCategory) {
                $http.post("backoffice/Gibe/LinkPickerApi/AddCategory", { Name: gtmCategory } ).success(function(data,status) {
                    return data;
                })
            },
            removeCategory: function(gtmCategory) {
                $http.post("backoffice/Gibe/LinkPickerApi/RemoveCategory", { Name: gtmCategory } ).success(function(data,status) {
                    return data;
                })
            }
        };
    }
);