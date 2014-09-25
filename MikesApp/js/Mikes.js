$(document).ready(function () {
    $('#dialog-form').dialog({
        autoOpen: false,
        height: 500,
        width: 400,
        modal: true,
        buttons: {
            "Add User": addUser ,
            Cancel: function () {
                $('#dialog-form').dialog("close");
            }
        },
        close: function () {
            $('#txtFirstName').val('');
            $('#txtLastName').val('');
            $('#txtPhone').val('');
            $('#txtAddress').val('');
            $('#txtEmail').val('');
        }
     });
    $('#btn-addusr').click(function () {
        $('#dialog-form').dialog("open");
    });

    $('#btnSearch').on('click', function () {
        updateResults();
    });


});

function updateResults() {
    $.get("/Home/GetUserInfo", { firstName: $('#txtFirstNameSearch').val(), lastName: $('#txtLastNameSearch').val() }, function (data) {
        $('#employee-results').html(data);
    });
}



function addUser() {
    var isValid = validateForm();
    if (isValid == true) {
        $.ajax({
            type: "POST",
            url: "/Home/SaveUserInfo",
            data: JSON.stringify({
                FirstName: $('#txtFirstName').val(),
                LastName: $('#txtLastName').val(),
                Phone: $('#txtPhone').val(),
                Address: $('#txtAddress').val(),
                Email: $('#txtEmail').val()
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8"
        })
       .done(function () {
           $('#txtFirstName').val('');
           $('#txtLastName').val('');
           $('#txtPhone').val('');
           $('#txtAddress').val('');
           $('#txtEmail').val('');
           updateResults();
           $('#dialog-form').dialog("close");
       });
    }
}

function validateForm() {
    var emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$/i;
    var phoneRegex = /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/;
    if ($('#txtFirstName').val().length == 0 || $('#txtLastName').val().length == 0 || $('#txtPhone').val().length == 0 || $('#txtAddress').val().length == 0 || $('#txtEmail').val().length == 0) {
        alert("All fields are required.");
        return false;
    }
    if(!emailRegex.test($('#txtEmail').val()))
    {
        alert("Please enter a valid email address");
        return false;
    }
    if(!phoneRegex.test($('#txtPhone').val()))
    {
        alert("Please enter a valid phone number.");
        return false;
    }
    else {
        return true;
    }
}