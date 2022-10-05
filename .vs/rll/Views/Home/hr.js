$('#pass, #pass1').on('keyup', function () {
    if ($('#pass').val() == $('#pass1').val()) {
        /* $('#message').html('Matching').css('color', 'green');*/
        $('#message').append("Password Matched")
    } else
    /* $('#message').html('Not Matching').css('color', 'red');*/
        $('#message').append("Password did not Matched")
});