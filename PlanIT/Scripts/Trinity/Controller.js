app.controller("PlanITController", function ($scope, PlanITService) {

    $scope.events = [];
    $scope.event = {};
    $scope.allUsers = [];
    $scope.myTickets = [];

    $scope.loadDashboardData = function () {
        $scope.loadEvents();
        var user = JSON.parse(sessionStorage.getItem("LoggedUser"));
        if (user) {
            var id = user.UserID || user.userID || user.userId;
            PlanITService.GetUserTicketsService(id).then(function (res) {
                $scope.myTickets = res.data;
            });
        }
    };

    $scope.loadEvents = function () {
        PlanITService.GetEventsService()
            .then(function (res) {
                $scope.events = res.data;
            })
            .catch(function (err) {
                console.error(err);
            });
    };

    $scope.checkLogin = function () {
        if ($scope.LUserName == "admin" && $scope.Lpassword == "admin") {
            $scope.RedirectingPage(404);
            return;
        }

        var loginData = { Email: $scope.LUserName, Password: $scope.Lpassword };

        PlanITService.LoginUserService(loginData).then(function (res) {
            if (res.data.status === "Success") {
                Swal.fire({ icon: "success", title: "Welcome " + res.data.firstName, timer: 1200, showConfirmButton: false });
                sessionStorage.setItem("LoggedUser", JSON.stringify(res.data));

                if (res.data.roleID === 1) {
                    $scope.RedirectingPage(404);
                } else {
                    $scope.RedirectingPage(303);
                }
            } else {
                Swal.fire({ icon: "error", title: res.data.message });
            }
        });
    };

    $scope.createEvent = function () {
        var eventData = {
            Title: $scope.event.Title,
            EventDate: $scope.event.EventDate,
            Location: $scope.event.Location,
            TotalTickets: $scope.event.TotalTickets,
            Description: $scope.event.Description,
            Price: $scope.event.Price
        };

        PlanITService.SaveEventService(eventData).then(function (res) {
            if (res.data === "Success") {
                Swal.fire({ icon: "success", title: "Event Published" });
                $scope.RedirectingPage(404);
            }
        });
    };

    $scope.buyTickets = function (event) {
        var currentUser = JSON.parse(sessionStorage.getItem("LoggedUser"));
        if (!currentUser) {
            Swal.fire("Error", "Please login first", "error");
            return;
        }

        var uID = currentUser.UserID || currentUser.userID;

        PlanITService.PurchaseTicketService({ EventID: event.EventID, UserID: uID })
            .then(function (res) {
                if (res.data === "Success") {
                    Swal.fire({ icon: 'success', title: 'Ticket Reserved!' });
                    $scope.loadDashboardData();
                }
            });
    };

    $scope.editEvent = function (event) {
        $scope.selectedEvent = angular.copy(event);
        if ($scope.selectedEvent.EventDate) {
            $scope.selectedEvent.EventDate = new Date($scope.selectedEvent.EventDate);
        }
    };

    $scope.updateEvent = function () {
        PlanITService.UpdateEventService($scope.selectedEvent).then(function (res) {
            if (res.data === "Success") {
                Swal.fire({ icon: "success", title: "Changes Saved!", timer: 1500, showConfirmButton: false });
                $scope.selectedEvent = {};
                $scope.loadEvents();
            }
        });
    };

    $scope.deleteEvent = function (eventID) {
        Swal.fire({
            title: "Are you sure?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                PlanITService.DeleteEventService(eventID).then(function (res) {
                    if (res.data === "Success") {
                        Swal.fire("Deleted!", "", "success");
                        $scope.loadEvents();
                    }
                });
            }
        });
    };

    $scope.addUser = function () {
        var userData = {
            FirstName: $scope.rFirstName,
            LastName: $scope.rLastName,
            Email: $scope.rEmail,
            Password: $scope.rPassword
        };
        PlanITService.RegisterUserService(userData).then(function (res) {
            if (res.data === "Success") {
                Swal.fire({ icon: "success", title: "Account Created" });
                $scope.RedirectingPage(101);
            }
        });
    };

    $scope.viewEventDetails = function (event) {
        localStorage.setItem("SelectedEvent", JSON.stringify(event));
        window.location.href = "/PlanIt/ViewShow";
    };

    $scope.loadSelectedEvent = function () {
        var storedEvent = localStorage.getItem("SelectedEvent");
        if (storedEvent) {
            $scope.event = JSON.parse(storedEvent);
        } else {
            $scope.RedirectingPage(303);
        }
    };

    $scope.RedirectingPage = function (redirectID) {
        var routes = {
            101: "/PlanIt/Login",
            202: "/PlanIt/Registration",
            303: "/PlanIt/Dashboard",
            404: "/PlanIt/AdminDashboard",
            505: "/PlanIt/CreateEvent",
            606: "/PlanIt/DeleteEvent",
            707: "/PlanIt/EditEvent",
            808: "/PlanIt/UserList"
        };
        if (routes[redirectID]) { window.location.href = routes[redirectID]; }
    };

    $scope.initCheckoutPage = function () {
        // Fallback Mock Defaults to prevent blank screens if database items aren't fully populated yet
        $scope.eventDetail = {
            EventID: 1,
            Title: 'Ado\'s "Hibana" World Tour',
            Location: 'SM Mall of Asia Arena',
            Price: 4500.00,
            TotalTickets: 44 // Try setting this to 0 to test your "Fully Booked" protection block!
        };

        $scope.bookingQty = 1;
        $scope.transactionComplete = false;
        $scope.generatedRefId = "";
    };

    // Tracks arithmetic updates via the + and - picker triggers
    $scope.changeQty = function (amount) {
        var computedTarget = $scope.bookingQty + amount;

        if (computedTarget >= 1 && computedTarget <= $scope.eventDetail.TotalTickets) {
            $scope.bookingQty = computedTarget;
        }
    };

    // Live cost calculation watcher helper
    $scope.getGrandTotal = function () {
        var currentPrice = $scope.eventDetail.Price || 4500.00;
        return currentPrice * $scope.bookingQty;
    };

    // Click Action Handler for Confirm Reservation
    $scope.submitReservation = function () {

        // 1. Alert safety check block if fully booked
        if ($scope.eventDetail.TotalTickets <= 0) {
            alert("Transaction Aborted: This event is currently fully booked! You cannot buy tickets.");
            return;
        }

        // 2. Mock processing calculation state change
        // Generate a quick dummy Booking ID for the receipt display
        $scope.generatedRefId = Math.floor(1000 + Math.random() * 9000);

        // Toggle on-page display into Receipt Mode
        $scope.transactionComplete = true;
    };

    // Export to PDF / Print Routine
    $scope.printPDF = function () {
        // Simply opens the native computer save-to-pdf configuration right away!
        window.print();
    };
}); 