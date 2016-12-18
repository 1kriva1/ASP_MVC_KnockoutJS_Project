$(function () {
    var pull = document.getElementById('pull');
    var menu = document.getElementById('mmenu');

    pull.onclick = function () {
        menu.classList.toggle('y');
    };

    $(document).click(function (event) {
        if (!$(event.target).closest('#pull').length) {
            menu.classList.remove('y');
        }
    })    

});