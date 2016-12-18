/// <reference path="../Scripts/jquery-2.1.4.js" />
/// <reference path="../Scripts/knockout-3.3.0.js" />
/// <reference path="../Scripts/knockout.mapping-latest.js" />


var courceList = function () {
    var self = this;
    self.feed = ko.mapping.fromJS(Context.Feed);
    self.search = ko.observable();

    self.search.subscribe(function () {
        delay(function () {
            self.reload();
        }, 300);
    });

    self.load = function () {
        callServer("GetCources/", {
            skip: self.feed.Skip(),
            search: self.search()
        }, function (data) {
            ko.mapping.fromJS(data, { ignore: ["Items"] }, self.feed);
            for (var i = 0; i < data.Items.length; i++) {
                self.feed.Items.push(data.Items[i]);
            }
        });
    };

    self.reload = function () {
        self.feed.Skip(0);
        self.feed.Items.removeAll();
        self.load();
    };

    self.showMore = function () {
        self.load();
    };

    self.load();
};

$(document).ready(function () {
    ko.applyBindings(new courceList());
});


