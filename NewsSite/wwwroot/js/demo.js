$(function () {
    $.ajax({
        url: '/api/user/GetUsers',
        method: 'GET'
    })
        .done(function (result) {
            var dropdown = $("#userdropdown")
            dropdown.html('');
            $.each(result, function (k, user) {

                dropdown.append('<option value="' + user.userName + '">' + user.userName + '</option>');

            });
            console.log(result);
        })
            
       
        .fail(function (xhr, status, error) {
            alert(`Fail!`)
            console.log("Error", xhr, status, error);
        })
});

$(document).on('click', '#populate-database', function () {

    $.ajax({
        url: '/api/database/seed',
        method: 'GET'
    })
        .done(function (result) {
            var dropdown = $("#userdropdown")
            dropdown.html('');
            $.each(result, function (k, user) {

                dropdown.append('<option value="' + user.userName + '">' + user.userName + '</option>');

            });
            console.log(result);

        })
        .fail(function (xhr, status, error) {
            alert("Fail");
            console.log("Error", xhr, status, error);
        })
});

$(document).on('click', '#sign-in', function () {
    var userName = $('#userdropdown').val();
    console.log(userName);
    $.ajax({
        url: '/api/user/signin',
        method: 'POST',
        data: {
            "UserName": userName
        }
    })
        .done(function (result) {
            console.log(result);

        })
        .fail(function (xhr, status, error) {
            alert("Fail");
            console.log("Error", xhr, status, error);
        })
});
$(document).on('click', '#check-open-news', function () {
    
    $.ajax({
        url: '/check/view/OpenNews',
        method: 'GET',
      
    })
        .done(function (result) {
            console.log(result);
            console.log("hej");

        })
});


$(document).on('click', '#check-hidden-news', function () {

    $.ajax({
        url: '/check/view/HiddenNews',
        method: 'GET',

    })
        .done(function (result) {
            console.log(result);
            console.log("hej");

        })
});

$(document).on('click', '#check-adult-news', function () {

    $.ajax({
        url: '/check/view/AdultNews',
        method: 'GET',

    })
        .done(function (result) {
            console.log(result);

        })
});