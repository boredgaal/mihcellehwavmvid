export function initcookiesprovider() {

    var __obj = {

        cookiemap: function () {

            this.getCookie = function (cookiename) {

                let name = cookiename + "=";
                let decodedCookie = decodeURIComponent(document.cookie);
                let ca = decodedCookie.split(';');
                for (let i = 0; i < ca.length; i++) {
                    let c = ca[i];
                    while (c.charAt(0) == ' ') {
                        c = c.substring(1);
                    }
                    if (c.indexOf(name) == 0) {
                        return c.substring(name.length, c.length);
                    }
                }

                return null;
            };

            this.setCookie = function (cookiename, cookievalue, expirationdays) {

                var d = new Date();
                d.setTime(d.getTime() + (expirationdays * 24 * 60 * 60 * 1000));
                var expires = "expires=" + d.toUTCString();
                document.cookie = cookiename + "=" + cookievalue + ";" + expires + ";path=/";
            };
        }
    };

    return new __obj.cookiemap();
}