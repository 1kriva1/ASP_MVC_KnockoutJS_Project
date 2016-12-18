/// <reference path="../Scripts/jquery-2.1.4.js" />
/// <reference path="../Scripts/knockout-3.3.0.js" />
/// <reference path="../Scripts/knockout.mapping-latest.js" />
/// <reference path="../Scripts/knockout.validation.js" />
/// <reference path="../Scripts/jquery-ui-1.11.4.js" />
/// <reference path="../Scripts/datejs.js" />

ko.validation.init({ insertMessages: false });

function Cource() {
    var self = this;
    self.Name = ko.observable().extend({ required: true });
    self.StartDateString = ko.observable().extend({ required: true });
    self.FinishDateString = ko.observable().extend({ required: true });
    self.Mark = ko.observable();

    ko.mapping.fromJS(window.model, {}, self);
    
    self.availableStudents = ko.observableArray([]).extend({ minLength: 0 });
    self.currentStudents = ko.observableArray([]);
    self.selectedStudent = ko.observableArray([]);
    self.allSubjects = ko.observableArray([]);
    self.selectedSubject = ko.observableArray([]).extend({ required: true });

    var studentId;

    self.mainFormVisib = ko.observable(true);


    self.defaultCaption = ko.observable('Select student...');
    self.dynamicOptionsCaption = ko.computed(function () {
        return self.selectedStudent()
			? self.defaultCaption()
			: null;
    });

    var group = ko.validation.group([
        self.selectedSubject
    ]);

    self.enabled = ko.computed(function () {
        if (group().length == 0) {
            return true;
        }
        else {
            return false;
        }
    });

    group.showAllMessages(false);
   
    self.addCource = function () {
        if (group().length == 0) {
            self.SubjectId = self.selectedSubject().Id;
            self.SubjectName = self.selectedSubject().Name;
            self.SchoolId = self.selectedSubject().SchoolId;
            callServer("AddCource/", this, function (data) {
                window.location.href = '/Home/Cources';
            });
        }
        else {
            group.showAllMessages(true);
        }
    };

    
    self.updateCource = function () {
        self.selectedSubject(1);
        if (group().length == 0) {
            callServer("UpdateCource/", this, function (data) {
                window.location.href = '/Home/Cources';
            });
        }
        else {
            group.showAllMessages(true);
        }
    };

    self.getAvailableStudents = function () {
        callServer("GetStudentsOfSchool/",
            { schoolId: self.SchoolId, subjectId: self.SubjectId }, function (data) {
                ko.mapping.fromJS(data, {}, self.availableStudents);
            });
    };

    self.getCurrentStudents = function () {
        callServer("GetStudentsForCource/",
            { schoolId: self.SchoolId, subjectId: self.SubjectId }, function (data) {
                for (var i = 0; i < data.length; i++) {
                    data[i].Dob = new Date(data[i].Dob).toString('d/M/yyyy');
                }
                ko.mapping.fromJS(data, {}, self.currentStudents);
            });
    };

    self.getAllSubjects = function () {
        callServer("GetAvailableSubjects/",
            { schoolId: self.SchoolId }, function (data) {
                ko.mapping.fromJS(data, {}, self.allSubjects);
            });
    };

    self.getCurrentStudents();
    self.getAllSubjects();

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

    //Change mark
    self.changeMarkPopup = ko.observable(null);

    self.openChangeMark = function (student) {
        studentId = student.Id();
        self.Mark = student.Mark();
        self.mainFormVisib(false);
        self.changeMarkPopup(new ChangeMarkVm({ studId: studentId, mark: self.Mark }, function () {
            self.getCurrentStudents();
            self.closeChangeMark();
        }));
    };

    self.closeChangeMark = function () {
        self.changeMarkPopup(null);
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
        callServer("AddStudentToCource/",
            { courceId: window.model.Id, studentId: self.selectedStudent().Id() }, function () {
                success();
            });
    };
};

function DeleteStudentVm(studentOptions, success) {
    var self = this;
    self.title = ko.observable("DeleteStudentDialog");
    self.studentId = studentOptions;

    self.deletes = function () {
        callServer("DeleteStudentFromCource/",
            { courceId: window.model.Id, studentId: self.studentId }, function () {
                success();
            });
    };
};

function ChangeMarkVm(studentOptions, success) {
    var self = this;
    self.title = ko.observable("ChangeMark");
    self.studentId = studentOptions.studId;
    self.Mark = studentOptions.mark;

    self.change = function () {
        callServer("ChangeMark/",
            { courceId: window.model.Id, studentId: self.studentId, mark: self.Mark }, function () {
                success();
            });
    };
};

$(document).ready(function () {
    $(function () {
        $(".datepicker").datepicker({
            dateFormat: "d/mm/yy"
        });
    });
    ko.applyBindings(new Cource());
});