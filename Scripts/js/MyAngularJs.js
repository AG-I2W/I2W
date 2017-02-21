//var app = angular.module('myApp', ['ngGrid']);


var app = angular.module('myApp', ['angular.filter']);


app.service("PortfolioService", function ($http) {
    this.get = function () {
        var req = {
            url: "https://www.w3schools.com/angular/customers.php",
            headers: { 'Content-Type': 'application/json' },
            method: 'GET'
        }
        return $http(req);
    };
});


app.controller("GetPortfolioCtrl", function ($scope, $http, PortfolioService) {
    this.groupProperty = '';

    //var promiseGet = PortfolioService.get();

    $http.get("http://localhost:41380/PortfolioService.svc/portfolio")
    .then(function (response) {
        $scope.Portfolios = response.data;
    }, function (error) {
        alert(error.statusText);
    });

    //promiseGet.then(function (response) {
    //    $scope.Portfolios = response.data
    //},
    //function (error) {
    //    if (error.status === -1)
    //        alert('failure loading Portfolio', error);
    //    else
    //        alert(error.statusText);
    //});
});




app.filter('groupBy', function () {
    return function (list, group_by) {

        var filtered = [];
        var prev_item = null;
        var group_changed = false;
        // this is a new field which is added to each item where we append "_CHANGED"
        // to indicate a field change in the list
        var new_field = group_by + '_CHANGED';

        // loop through each item in the list
        angular.forEach(list, function (item) {

            group_changed = false;

            // if not the first item
            if (prev_item !== null) {

                // check if the group by field changed
                if (prev_item[group_by] !== item[group_by]) {
                    group_changed = true;
                }

                // otherwise we have the first item in the list which is new
            } else {
                group_changed = true;
            }

            // if the group changed, then add a new field to the item
            // to indicate this
            if (group_changed) {
                item[new_field] = true;
            } else {
                item[new_field] = false;
            }

            filtered.push(item);
            prev_item = item;

        });

        return filtered;
    };
});


//app.controller("CRUD_AngularJs_RESTController", function ($scope, CRUD_AngularJs_RESTService) {

//    $scope.OperType = 1;
//    //1 Mean New Entry  

//    GetAllRecords();
//    //To Get All Records  
//    function GetAllRecords() {
//        var promiseGet = CRUD_AngularJs_RESTService.getAllStudent();
//        promiseGet.then(function (pl) { $scope.Students = pl.data },
//              function (errorPl) {
//                  $log.error('Some Error in Getting Records.', errorPl);
//              });
//    }

//    //To Clear all input controls.  
//    function ClearModels() {
//        $scope.OperType = 1;
//        $scope.StudentID = "";
//        $scope.Name = "";
//        $scope.Email = "";
//        $scope.Class = "";
//        $scope.EnrollYear = "";
//        $scope.City = "";
//        $scope.Country = "";
//    }

//    //To Create new record and Edit an existing Record.  
//    $scope.save = function () {
//        var Student = {
//            Name: $scope.Name,
//            Email: $scope.Email,
//            Class: $scope.Class,
//            EnrollYear: $scope.EnrollYear,
//            City: $scope.City,
//            Country: $scope.Country
//        };
//        if ($scope.OperType === 1) {
//            var promisePost = CRUD_AngularJs_RESTService.post(Student);
//            promisePost.then(function (pl) {
//                $scope.StudentID = pl.data.StudentID;
//                GetAllRecords();

//                ClearModels();
//            }, function (err) {
//                console.log("Some error Occured" + err);
//            });
//        } else {
//            //Edit the record      
//            debugger;
//            Student.StudentID = $scope.StudentID;
//            var promisePut = CRUD_AngularJs_RESTService.put($scope.StudentID, Student);
//            promisePut.then(function (pl) {
//                $scope.Message = "Student Updated Successfuly";
//                GetAllRecords();
//                ClearModels();
//            }, function (err) {
//                console.log("Some Error Occured." + err);
//            });
//        }
//    };

//    //To Get Student Detail on the Base of Student ID  
//    $scope.get = function (Student) {
//        var promiseGetSingle = CRUD_AngularJs_RESTService.get(Student.StudentID);
//        promiseGetSingle.then(function (pl) {
//            var res = pl.data;
//            $scope.StudentID = res.StudentID;
//            $scope.Name = res.Name;
//            $scope.Email = res.Email;
//            $scope.Class = res.Class;
//            $scope.EnrollYear = res.EnrollYear;
//            $scope.City = res.City;
//            $scope.Country = res.Country;
//            $scope.OperType = 0;
//        },
//                  function (errorPl) {
//                      console.log('Some Error in Getting Details', errorPl);
//                  });
//    }

//    //To Delete Record  
//    $scope.delete = function (Student) {
//        var promiseDelete = CRUD_AngularJs_RESTService.delete(Student.StudentID);
//        promiseDelete.then(function (pl) {
//            $scope.Message = "Student Deleted Successfuly";
//            GetAllRecords();
//            ClearModels();
//        }, function (err) {
//            console.log("Some Error Occured." + err);
//        });
//    }
//});

//app.service("RESTService", function ($http) {
//    //Create new record  
//    this.post = function (Student) {
//        var request = $http({
//            method: "post",
//            url: "http://localhost:27321/StudentService.svc/AddNewStudent",
//            data: Student
//        });
//        return request;
//    }

//    //Update the Record  
//    this.put = function (StudentID, Student) {
//        debugger;
//        var request = $http({
//            method: "put",
//            url: "http://localhost:27321/StudentService.svc/UpdateStudent",
//            data: Student
//        });
//        return request;
//    }

//    this.getAllStudent = function () {
//        return $http.get("http://localhost:27321/StudentService.svc/GetAllStudent");
//    };

//    //Get Single Records  
//    this.get = function (StudentID) {
//        return $http.get("http://localhost:27321/StudentService.svc/GetStudentDetails/" + StudentID);
//    }

//    //Delete the Record  
//    this.delete = function (StudentID) {
//        var request = $http({
//            method: "delete",
//            url: "http://localhost:27321/StudentService.svc/DeleteStudent/" + StudentID
//        });
//        return request;
//    }
//});

//app.controller('GetPortfolioCtrl', function ($scope, $http) {
//    $http({
//        method: 'Get',
//        url: "http://localhost:41380/PortfolioService.svc",
//        datatype:JSON,
//        headers: {  
//            "Content-Type": "application/json"  
//        }  
//    }).success(function (data) {
//        $scope.item = data;
//    }).error(function (error) {
//    });

//    @scope.gridOptions ={
//        data:'items',
//        columnDefs:[
//        {field:'Title', displayName:'Title'}
//        ]
//    };
//});


//var app = angular.module('myApp', ['ngRoute']);
//var app = angular.module('myApp', []);

//app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
//    $routeProvider.when('/showemployees',
//    {
//        templateUrl: 'EmployeeInfo/ShowEmployees',
//        controller: 'HomeController'
//    });
//    $routeProvider.when('/addemployee',
//    {
//        templateUrl: 'EmployeeInfo/AddNewEmployee',
//        controller: 'HomeController'
//    });
//    $routeProvider.when("/editemployee",
//    {
//        templateUrl: 'EmployeeInfo/EditEmployee',
//        controller: 'HomeController'
//    });
//    $routeProvider.when('/Contacts',
//    {
//        templateUrl: 'Home/Contacts',
//        controller: 'HomeController'
//    });
//    $routeProvider.otherwise(
//    {
//        redirectTo: '/'
//    });
//    // $locationProvider.html5Mode(true);
//    $locationProvider.html5Mode(true).hashPrefix('!')
//}]);


//app.controller('myCtrl', function($scope) {
//});


//$("#Services").click(function () {
//});

//app.controller('addMore', function ($scope) {
//    $scope.contact = []
//    $scope.addwebsite = function () {
//        $scope.contact.push({
//            $scope.contact
//        })
//    }
//});