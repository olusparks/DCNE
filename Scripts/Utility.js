$(document).ready(function () {
    $('.date').datepicker({
        changeYear: true,
        changeMonth: true,
        dateFormat: "yy-mm-dd"
    });

    $(".thumb").elevateZoom({
        scrollZoom: true,
    });

    function beginRequest() {
        $("#changePicture").hide();
    }

    function endRequest() {
        $("#changePicture").show();
    }

    $('#inputMessage').focus();
    var name = document.getElementById("inputMessage");
    var category = document.getElementById("inputCate");
    var location = document.getElementById("inputLocation");
    $("#btnSubmit").prop("disabled", true);
    $(location, category, name).bind('change keyup', function () {
        if ($(name).val().length > 0 && $(category).val().length > 0 && $(location).val().length > 0) {
            $("#btnSubmit").prop("disabled", false);
        }
        else {
            $("#btnSubmit").prop("disabled", true);
        }
    })

   /* function OnComplete() {
        alert('The Complete Section');
    }

    function OnSuccess() {
        var getButton = document.getElementById("btnSubmit");
        $(getButton).on('submit', function () {
            var getMessage = document.getElementById("inputMessage");
            var getCategory = document.getElementById("inputCate");
            var getLocation = document.getElementById("inputLocation");

            var theMessage = $(getMessage).val();
            var theCategory = $(getCategory).val();
            var theLocation = $(getLocation).val();

            alert(theMessage + "<br />" + theCategory + "&nsbp &nsbp" + theLocation);
        });
    }*/


});



//====================================Comment===========================



/*
 function SuccessMessage() {
        $("#btnSubmit").on('click', function () {
            var myModal = document.getElementById("myModal");
            $(myModal).modal('toggle', {
                backdrop: "static",
                keyboard: false,
                show: true
            });
        });
    }

 $('#inputMessage', '#inputCate', '#inputLocation').bind('change', function () {
        if ($('#inputMessage').val() != '' &&
            $('#inputCate :selected').text() != '' &&
            $('#inputLocation :selected').text() != '') {
            alert($('#inputCate :selected').text());
            $(this).closest('form').find(':submit').removeAttr('disabled');
        }
        else {
            $(this).closest('form').find(':submit').attr('disabled', 'disabled');
        }
    });

 validate();
    $('#inputMessage', '#inputCate', '#inputLocation').bind('change', validate());
    function validate() {
        if ($('#inputMessage').val().length > 0 &&
            $('#inputCate').val().length > 0 &&
            $('#inputLocation').val().length > 0) {
            $("#btnSubmit").prop("disabled", false);
        }
        else {
            $("#btnSubmit").prop("disabled", true);
        }
    }


$('#inputMessage', '#inputCate', '#inputLocation').keyup(function () {

        var empty = false;
        $('#inputMessage', '#inputCate', '#inputLocation').each(function () {
            if ($(this).val() === '') {
                empty = true;
            }
        });

        if (empty) {
            $('#btnSubmit').attr('disabled', 'disabled');
        } else {
            $('#btnSubmit').prop("disabled", false);
        }
    });

/
*/

/*
$('#complaintForm').submit(function (event) {
        event.preventDefault();

        var form = $(this);
        var submitButton = $('button[type=submit]', form);

        $.ajax({
            type: "Post",
            url: "/Complaint/Create",
            data: $("complaintForm").serialize(),
            accept : { javascript: 'application/javascript' },
                
            dataType: "html",
            beforeSend: function () {
                $("loadingPhoto").show();
                submitButton.prop('disabled', 'disabled');
            },
            complete: function () {
                $("#loadingPhoto").hide();
            },
            success: function (data) {
                $("myModal").modal('toggle');
            }
        }).done(function (data) {
            submitButton.prop('disabled', false);
            $('#myModal').modal({'show' : true});
        });
    });*/