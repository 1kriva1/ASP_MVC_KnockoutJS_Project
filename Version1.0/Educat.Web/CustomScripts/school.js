/// <reference path="school.js" />
/// <reference path="../Scripts/jquery-2.1.4.js" />
/// <reference path="../Scripts/knockout-3.3.0.js" />
/// <reference path="../Scripts/knockout.mapping-latest.js" />
/// <reference path="../Scripts/knockout.validation.js" />

ko.validation.init({ insertMessages: false });

function School() {
    var self = this;

    self.Name = ko.observable().extend({ required: true }); // name of school
    self.Students = ko.observableArray([]); // array for school's students
    self.Subjects = ko.observableArray([]);// array for school's cources

    var studentId, courceId;
    self.newSubject = ko.observable();
    self.availableStudents = ko.observableArray([]);
    self.selectedStudent = ko.observableArray([]);

    self.defaultCaption = ko.observable('Select student...');
    self.dynamicOptionsCaption = ko.computed(function () {
        return self.selectedStudent()
			? self.defaultCaption()
			: null;
    });

    /*visibility variable*/
    self.mainFormVisib = ko.observable(true);

    ko.mapping.fromJS(window.model, {}, self);

    var group = ko.validation.group([
        self.Name
    ]);

    group.showAllMessages(false);

    self.addSchool = function () {
        if (group().length == 0) {
            callServer("AddSchool/", this, function (data) {
                window.location.href = '/Home/Schools';
            });
        }
        else {
            group.showAllMessages(true);
        }
    }

    self.updateSchool = function () {
        if (group().length == 0) {
            callServer("UpdateSchool/", this, function (data) {
                window.location.href = '/Home/Schools';
            });
        }
        else {
            group.showAllMessages(true);
        }
    };

    //get all students of current school
    self.getAllStudents = function () {
        callServer("GetAllStudents/", { id: window.model.Id }, function (data) {
            ko.mapping.fromJS(data, {}, self.Students);
        });

    };

    //get all cources of current school
    self.getAllSubjects = function () {
        callServer("GetAllSubjects/", { Id: window.model.Id }, function (data) {
            ko.mapping.fromJS(data, {}, self.Subjects);
        });
    };

    self.getAvailableStudents = function () {
        callServer("GetAvailableStudents/", { id: window.model.Id }, function (data) {
            ko.mapping.fromJS(data, {}, self.availableStudents);
        });
    };

    self.getAllSubjects();
    self.getAllStudents();

    // Popups

    //1. Add Student
    self.addStudentPopup = ko.observable(null);

    self.openAddStudent = function () {
        self.getAvailableStudents();
        self.mainFormVisib(false)

        self.addStudentPopup(new AddStudentVm(self.availableStudents, function () {
            self.mainFormVisib(true);
            self.closeAddStudent();
            self.getAllStudents();
        }));
    };

    self.closeAddStudent = function () {
        self.addStudentPopup(null);
        self.mainFormVisib(true);
    };

    //2. Add Subject
    self.addSubjectPopup = ko.observable(null);

    self.openAddSubject = function () {
        self.mainFormVisib(false);
        self.addSubjectPopup(new AddSubjectVm(self.newSubject, function () {
            self.closeAddSubject();
            self.getAllSubjects();
            self.mainFormVisib(true);
        }));
    };

    self.closeAddSubject = function () {
        self.addSubjectPopup(null);
        self.mainFormVisib(true);
    };

    // 3. Delete Student
    self.deleteStudentPopup = ko.observable(null);

    self.openDeleteStudent = function (student) {
        studentId = student.Id();
        self.mainFormVisib(false);
        self.deleteStudentPopup(new DeleteStudentVm(studentId, function () {
            self.mainFormVisib(true);
            self.getAllStudents();
            self.closeDeleteStudent();
        }));
    };

    self.closeDeleteStudent = function () {
        self.deleteStudentPopup(null);
        self.mainFormVisib(true);
    };

    //4. Delete Subject
    self.deleteSubjectPopup = ko.observable(null);

    self.openDeleteSubject = function (subject) {
        subjectId = subject.Id();
        self.mainFormVisib(false);
        self.deleteSubjectPopup(new DeleteSubjectVm(subjectId, function () {
            self.mainFormVisib(true);
            self.getAllSubjects();
            self.closeDeleteSubject();
        }));
    };

    self.closeDeleteSubject = function () {
        self.deleteSubjectPopup(null);
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
        callServer("AddStudentToSchool/",
            { schoolId: window.model.Id, studentId: self.selectedStudent().Id() }, function (data) {
            success();
        });
    };
};

function AddSubjectVm(subjectOptions, success) {
    var self = this;
    self.title = ko.observable("AddCourceDialog");
    self.newSubject = subjectOptions;

    self.add = function () {
        callServer("AddSubjectToSchool/",
            { schoolId: window.model.Id, newSubject: self.newSubject() }, function (data) {
            success();
        });
    };
};

function DeleteStudentVm(studentOptions, success) {
    var self = this;
    self.title = ko.observable("DeleteStudentDialog");
    self.studentId = studentOptions;

    self.deletes = function () {
        callServer("DeleteStudent/", { studentId: self.studentId, schoolId: window.model.Id }, function (data) {
            success();
        });
    };
};

function DeleteSubjectVm(subjectOptions, success) {
    var self = this;
    self.title = ko.observable("DeleteCourceDialog");
    self.subjectId = subjectOptions;

    self.deletes = function () {
        callServer("DeleteSubject/", { subjectId: self.subjectId, schoolId: window.model.Id }, function (data) {
            success();
        });
    };
};

$(document).ready(function () {
    ko.applyBindings(new School());
});