/// <reference path="../Scripts/jquery-2.1.4.js" />
/// <reference path="../Scripts/knockout-3.3.0.js" />
/// <reference path="../Scripts/knockout.mapping-latest.js" />
/// <reference path="../Scripts/knockout.validation.js" />

ko.validation.init({ insertMessages: false });

function Student() {
    var self = this;

    self.FirstName = ko.observable().extend({ required: true });
    self.LastName = ko.observable().extend({ required: true });
    self.Email = ko.observable().extend({ required: true, email: true });
    self.DobString = ko.observable();

    ko.mapping.fromJS(window.model, {}, self);
    var group = ko.validation.group([
        self.FirstName,
        self.LastName,
        self.Email,
    ]);

    group.showAllMessages(false);    

    self.addStudent = function () {
        if (group().length == 0) {
            callServer("AddStudent/", this, function (data) {                
                self.cleanInputs()
            });            
        }
        else {
            group.showAllMessages(true);
        }
    }

    self.updateStudent = function () {
        if (group().length == 0) {
            callServer("UpdateStudent/", this, function (data) {
                self.cleanInputs();
            });            
        }
        else {
            group.showAllMessages(true);
        }
    }

    self.cleanInputs = function () {       
        window.location.href = '/Home/Students';
    };
}



$(document).ready(function () {
    $(function () {
        $(".datepicker").datepicker({
            dateFormat: "d/mm/yy"
        });
    });
    ko.applyBindings(new Student());        
});