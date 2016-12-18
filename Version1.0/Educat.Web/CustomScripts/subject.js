/// <reference path="../Scripts/jquery-2.1.4.js" />
/// <reference path="../Scripts/knockout-3.3.0.js" />
/// <reference path="../Scripts/knockout.mapping-latest.js" />
/// <reference path="../Scripts/knockout.validation.js" />

ko.validation.init({ insertMessages: false });

function Subject() {
    var self = this;
    self.Name = ko.observable().extend({ required: true });
    ko.mapping.fromJS(window.model, {}, self);

    self.availableStudents = ko.observableArray([]).extend({ minLength: 0 });
    self.currentStudents = ko.observableArray([]);
    self.selectedStudent = ko.observableArray([]);
    var studentId;
    
    self.mainFormVisib = ko.observable(true);


    self.defaultCaption = ko.observable('Select student...');
    self.dynamicOptionsCaption = ko.computed(function () {
        return self.selectedStudent()
			? self.defaultCaption()
			: null;
    });

    var group = ko.validation.group([
        self.Name
    ]);

    group.showAllMessages(false);

    self.addSubject = function () {
        if (group().length == 0) {
            callServer("AddSubject/", this, function (data) {
                window.location.href = '/Home/Subjects';
            });
        }
        else {
            group.showAllMessages(true);
        }
    };

    self.updateSubject = function () {
        if (group().length == 0) {
            callServer("UpdateSubject/", this, function (data) {
                window.location.href = '/Home/Subjects';
            });
        }
        else {
            group.showAllMessages(true);
        }
    };

    self.getAvailableStudents = function () {
        callServer("GetStudentsOfSchool/",
            { schoolId: window.model.SchoolId, subjectId: window.model.Id }, function (data) {
            ko.mapping.fromJS(data, {}, self.availableStudents);
        });
    };

    self.getCurrentStudents = function () {
        callServer("GetCurrentStudents/",
            { schoolId: window.model.SchoolId, subjectId: window.model.Id }, function (data) {
            ko.mapping.fromJS(data, {}, self.currentStudents);
        });
    };

    //self.getCurrentStudents();

    // Popups

    //1. Add Student
    self.addStudentPopup = ko.observable(null);

    self.openAddStudent = function () {
        self.getAvailableStudents();
        self.mainFormVisib(false);

        self.addStudentPopup(new AddStudentVm(self.availableStudents, function () {
            self.closeAddStudent();            
            self.getCurrentStudents();
        }));
    };

    self.closeAddStudent = function () {
        self.addStudentPopup(null);
        self.mainFormVisib(true);
    };

    // 2. Delete Student
    self.deleteStudentPopup = ko.observable(null);

    self.openDeleteStudent = function (student) {
        studentId = student.Id();
        self.mainFormVisib(false);
        self.deleteStudentPopup(new DeleteStudentVm(studentId, function () {
            self.getCurrentStudents();
            self.closeDeleteStudent();
            
        }));
    };

    self.closeDeleteStudent = function () {
        self.deleteStudentPopup(null);
        self.mainFormVisib(true);
    };
};

function AddStudentVm(studentOptions, success) {
    var self = this;
    self.title = ko.observable("AddStudentDialog");

    
        
    self.availableStudents = studentOptions;
    self.selectedStudent = ko.observable().extend({ required: true });
    var group = ko.validation.group([
        self.selectedStudent
    ]);

    self.enabled = ko.computed(function () {
        if (group().length == 0) {
            return true;            
        }
        else {
            return false;            
        }
    });

    self.add = function () {
        callServer("AddStudentToSubject/",
            { subjectId: window.model.Id, studentId: self.selectedStudent().Id() }, function () {
                success();
            });
    };
};

function DeleteStudentVm(studentOptions, success) {
    var self = this;
    self.title = ko.observable("DeleteStudentDialog");
    self.studentId = studentOptions;

    self.deletes = function () {
        callServer("DeleteStudentFromSubject/",
            { subjectId: window.model.Id, studentId: self.studentId }, function () {
            success();
        });
    };
};

$(document).ready(function () {
    ko.applyBindings(new Subject());
});