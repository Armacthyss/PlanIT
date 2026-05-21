app.service("PlanITService", function ($http) {

    this.UpsertService = function () {
        return $http({
            url: "/PlanIt/UpsertUsers",
            method: "POST"
        });
    };

    this.RegisterUserService = function (userData) {
        return $http({
            url: "/PlanIt/RegisterUser",
            method: "POST",
            data: userData
        });
    };

    this.LoginUserService = function (loginData) {
        return $http({
            url: "/PlanIt/LoginUser",
            method: "POST",
            data: loginData
        });
    };

    this.GetAllUsersService = function () {
        return $http.get("/PlanIt/GetAllUsers");
    };

    this.SaveEventService = function (eventData) {
        return $http({
            url: "/PlanIt/SaveEvent",
            method: "POST",
            data: eventData
        });
    };

    this.GetEventsService = function () {
        return $http({
            url: "/PlanIt/GetEvents",
            method: "GET"
        });
    };

    this.UpdateEventService = function (eventData) {
        return $http.post("/PlanIt/UpdateEvent", eventData);
    };

    this.DeleteEventService = function (id) {
        return $http.post("/PlanIt/DeleteEventData", { EventID: id });
    };

    this.PurchaseTicketService = function (bookingData) {
        return $http({
            url: "/PlanIt/PurchaseTicket",
            method: "POST",
            data: bookingData
        });
    };

    this.GetUserTicketsService = function (userID) {
        return $http({
            url: "/PlanIt/GetUserTickets?UserID=" + userID,
            method: "GET"
        });
    };

});