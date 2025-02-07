let auth = {
    async signUp() {
        $('#btnSignup').prop('disabled', true);

        let userName = $('#txtUserName').val()?.trim(); // Trim whitespace
        let email = $('#txtUserEmail').val();
        let password = $('#txtUserPassword').val();

        // Validate userName
        if (!userName) {
            global.toggleAlert(2, "User name is required and cannot be empty.", "#txtUserName");
            return;
        }

        // Additional Title Validation: Minimum and Maximum Length
        if (userName.length < 5 || userName.length > 30) {
            global.toggleAlert(2, "User name must be between 5 and 30 characters.", "#txtUserName");
            return;
        }

        // Validate email
        if (!email) {
            global.toggleAlert(2, "Email is required and cannot be empty.", "#txtUserEmail");
            return;
        }

        // Validate password
        if (!password) {
            global.toggleAlert(2, "Password is required and cannot be empty.", "#txtUserPassword");
            return;
        }

        // Prepare data object
        let signupData = {
            UserName: userName,
            Email: email,
            Password: password
        };

        try {
            // Send data to server
            const response = await fetch('/Signup/Signup', { // Add leading slash for correct URL
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(signupData),
            });

            if (response.ok) {
                // 200-299 Success Response
                const result = await response.text(); // Or response.json() if it's JSON
                auth.clearSignupInputs();
                global.toggleAlert(0, "Registration successful! Congratulations, you're now part of better society.", "");
                window.open("/Login", "_self");
            }
            else if (response.status === 400) {
                // 400 Bad Request (Validation errors)
                let errorResponse;
                try {
                    errorResponse = await response.json(); // Attempt to parse JSON
                }
                catch {
                    errorResponse = { message: await response.text() }; // Fallback for plain text errors
                }

                const errorMessages = errorResponse?.Errors?.join('\n') || errorResponse?.message || "Invalid request.";
                global.toggleAlert(1, `${errorMessages}`, "");
            }
            else if (response.status === 500) {
                // 500 Internal Server Error
                global.toggleAlert(1, "Server error. Please try again later.", "");
            }
            else {
                // Other unexpected statuses
                global.toggleAlert(1, `Unexpected error: ${response.statusText}`, "");
            }
        }
        catch (error) {
            // Network errors (e.g., no internet, server down)
            console.error("Network Error:", error);
            global.toggleAlert(1, "Network error. Please check your internet connection.", "");
        }

        $('#btnSignup').prop('disabled', false);
    },

    clearSignupInputs() {
        $('#txtUserName, #txtUserEmail, #txtUserPassword').val('');
    },

    async login() {
        let userName = $('#txtUserNameOrEmail').val()?.trim(); // Trim whitespace
        let email = $('#txtUserNameOrEmail').val();
        let password = $('#txtUserPassword').val();

        // Validate userName
        if (!userName) {
            global.toggleAlert(2, "User name or Email is required and cannot be empty.", "#txtUserNameOrEmail");
            return;
        }

        // Validate password
        if (!password) {
            alert("");
            global.toggleAlert(2, "Password is required and cannot be empty.", "#txtUserPassword");
            return;
        }

        //#region unused code

        //// Additional Title Validation: Minimum and Maximum Length
        //if (userName.length < 5 || userName.length > 30) {
        //    alert("Title must be between 5 and 30 characters.");
        //    return;
        //}

        //// Validate email
        //if (!email) {
        //    alert("Email is required and cannot be empty.");
        //    return;
        //}

        //#endregion

        // Prepare data object
        let loginData = {
            UserName: userName,
            Email: email,
            Password: password
        };

        try {
            // Send data to server
            const response = await fetch('/Login/Login', { // Add leading slash for correct URL
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(loginData),
            });

            // Handle the response
            if (response.ok) {
                //const result = await response.json();
                const result = await response.text(); // Response might not always be JSON

                //#region Save Token
                //const token = response.token; // Extract the token from the API response
                //const expires = new Date(Date.now() + 60 * 60 * 1000).toUTCString(); // 1 hour expiry
                //// Set the cookie
                //document.cookie = `token=${token}; expires=${expires}; path=/; secure; samesite=strict`;

                /*
                    Explanation of Cookie Attributes :
                    expires: Sets when the cookie should expire. Use UTC format.
                    path=/: Ensures the cookie is accessible on all paths of the site.
                    secure: Ensures the cookie is only sent over HTTPS (effective in production).
                    samesite=strict: Prevents the cookie from being sent with cross-site requests, reducing CSRF risks.
                */
                //#endregion

                $('#txtUserName, #txtUserEmail, #txtUserPassword').val('');
                //redirect to Feed
                window.open("/User/AskQuestion", "_self");

            }
            else {
                global.toggleAlert(1, "Unexpected error : " + JSON.stringify(errorResponse), "");
            }
        }
        catch (error) {
            console.error("Error:", error);
            global.toggleAlert(1, "An unexpected error occurred. Please try again later.", "");
        }
    },

    getCookie(name) {
        const cookies = document.cookie.split(';'); // Split cookies into individual key-value pairs
        for (let i = 0; i < cookies.length; i++) {
            const cookie = cookies[i].trim(); // Trim leading/trailing spaces
            if (cookie.startsWith(name + '=')) {
                return cookie.substring((name + '=').length); // Extract the cookie value
            }
        }
        return null; // Return null if the cookie is not found
    },
};

let user = {
    async createBlogPost() {
        try {
            // Gather data from the form or inputs
            let title = $("#txtTitle").val()?.trim();
            let postDetail = $("#txtPostDetail").val()?.trim();

            // Validate Title
            if (!title) {
                alert("Title is required and cannot be empty.");
                return;
            }

            // Additional Title Validation: Minimum and Maximum Length
            if (title.length < 5 || title.length > 100) {
                alert("Title must be between 5 and 100 characters.");
                return;
            }

            // Validate Title
            if (!postDetail) {
                alert("Post Content is required and cannot be empty.");
                return;
            }

            // Prepare data object
            let blogPostData = {
                title: title,
                QuestionDetail: postDetail
                // Include other properties if needed, e.g., createdOn, createdBy, etc.
            };

            // Send data to server
            const response = await fetch('/User/BlogPost/CreateBlogPost', {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                    //'X-XSRF-TOKEN': auth.getCookie("XSRF-TOKEN")
                },
                body: JSON.stringify(blogPostData),
            });

            // Handle the response
            if (response.ok) {
                const result = await response.json();
                alert("Blog post created successfully!");
                console.log(result);
            }
            else {
                const errorResponse = await response.json();
                alert("Error: " + JSON.stringify(errorResponse));
            }
        }
        catch (e) {
            console.error("An error occurred:", e);
            alert("An error occurred while saving the blog post. Please try again.");
        }
    },
};

let global = {
    async executeOrder(button, asyncFunction, ...args) {
        try {
            global.toggleLoader(1);
            // Execute the provided async function with arguments
            await asyncFunction(...args);

            //how to use ...args
            /* function exampleFunction(...args) {
                console.log(args); // This will log all the arguments passed to the function in an array.
            }
            exampleFunction(1, 2, 3, "hello", true); */

            global.toggleLoader(0);
        }
        catch (error) {
            console.error("An error occurred:", error);
            global.toggleAlert(1, "An unexpected error occurred. Please try again later.", "");
        }
        finally {
            //do something here
        }
    },

    toggleLoader(prm) {
        switch (prm) {
            case 1:
                if ($("#better-loader-overlay").length === 0) { // Check if loader already exists
                    $("body").append(`
                    <div id="better-loader-overlay">
                        <div class="better-loader-container">
                            <i class="las la-cog better-spinner"></i>
                            Please fxckxng wait!
                        </div>
                    </div>`);
                }
                $("#better-loader-overlay").show();
                break;
            case 0:
                $("#better-loader-overlay").hide();
                break;
        }
    },

    toggleAlert(_prm, _message, _inputToFocus) {
        /*
            alert-primary (Blue)
            alert-secondary (Grey)
            alert-success (Green)
            alert-danger (Red)
            alert-warning (Yellow)
            alert-dark (dark grey)
        */

        //[0,1,2,3]
        let _class = ["alert-success", "alert-danger", "alert-warning", "alert-primary"];
        let _iconClass = ["la-check", "la-skull-crossbones", "la-exclamation-triangle", "la-info-circle"];

        if ($("#better-alert").length === 0) { // Check if alert already exists
            $("body").append(`
            <div id='better-alert' class="alert ${_class[_prm]}" role="alert">
                <i id='better-alert-icon' class='las ${_iconClass[_prm]}' aria-hidden='true'></i>
                <span id='better-alert-message'>${_message}</span>
            </div>`);
        }
        else {
            $("#better-alert").removeClass().addClass(`alert ${_class[_prm]}`);
            $("#better-alert-icon").removeClass().addClass(`las ${_iconClass[_prm]}`);
            $("#better-alert-message").html(_message);
        }

        $("#better-alert").fadeIn(300).delay(7000).fadeOut(500);

        if (_inputToFocus) {
            $(_inputToFocus).focus();
        }

        /* This is a primary alert with <a href="#" class="alert-link">an example link</a>. Give it a click if you like. */
    },
};