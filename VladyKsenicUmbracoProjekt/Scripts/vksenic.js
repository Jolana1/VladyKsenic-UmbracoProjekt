$(document).ready(function () {
    // Contact
    vksenicContactFormApi();
    //// Menu bar
    //naplnspajzuMenuScrollTrigger();
    //// Cookies
    //if ($.cookie("naplnspajzu_cookies_ok") != '1') {
    //    $('.cookies-div').show();
    //}
});

/* contact */
function vksenicContactFormApi() {
    if ($('.api-password-group').length > 0) {
        $.ajax('/Umbraco/OsobnaStranka/OsobnaStrankaApi/ContactFormApiKey',
            {
                type: 'POST',
                success: function (data) {
                    $('.api-password-group #Password').val(data.MainKey);
                    $('.api-password-group #ConfirmPassword').val(data.SubKey);
                }
            });
    }
}