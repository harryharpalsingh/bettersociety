let user = {
    async signUp() {
        let userName = $('#txtUserName').val()?.trim(); // Trim whitespace
        let email = $('#txtUserEmail').val();
        let password = $('#txtUserPassword').val();

        // Validate userName
        if (!userName) {
            alert("User name is required and cannot be empty.");
            return;
        }

        // Additional Title Validation: Minimum and Maximum Length
        if (userName.length < 5 || userName.length > 30) {
            alert("Title must be between 5 and 30 characters.");
            return;
        }

        // Validate email
        if (!email) {
            alert("Email is required and cannot be empty.");
            return;
        }

        // Validate password
        if (!password) {
            alert("Password is required and cannot be empty.");
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
            const response = await fetch('/SignUp/Signup', { // Add leading slash for correct URL
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(signupData),
            });

            // Handle the response
            if (response.ok) {
                //const result = await response.json();
                const result = await response.text(); // Response might not always be JSON
                //console.log(result);

                $('#txtUserName, #txtUserEmail, #txtUserPassword').val('');
                alert("User created successfully!");
                window.open("/Login");
                //redirect to login page
            }
            else {
                //const errorResponse = await response.json();
                //alert("Error: " + JSON.stringify(errorResponse));

                // Error case
                const errorResponse = await response.json();
                const errorMessages = errorResponse?.map(err => err.description).join('\n') || "An error occurred.";
                alert("Error: " + errorMessages);
            }
        }
        catch (error) {
            console.error("Error:", error);
            alert("An unexpected error occurred. Please try again later.");
        }
    },

    async login() {
        let userName = $('#txtUserNameOrEmail').val()?.trim(); // Trim whitespace
        let email = $('#txtUserNameOrEmail').val();
        let password = $('#txtUserPassword').val();

        // Validate userName
        if (!userName) {
            alert("User name or Email is required and cannot be empty.");
            return;
        }

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

        // Validate password
        if (!password) {
            alert("Password is required and cannot be empty.");
            return;
        }

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
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(loginData),
            });

            // Handle the response
            if (response.ok) {
                //const result = await response.json();
                const result = await response.text(); // Response might not always be JSON
                //console.log(result);

                $('#txtUserName, #txtUserEmail, #txtUserPassword').val('');
                alert("Login successfull!");
                window.open("/User/Home", "_self");
                //redirect to User -> Home
            }
            else {
                const errorResponse = await response.json();
                alert("Error: " + JSON.stringify(errorResponse));

                // Error case
                //const errorResponse = await response.json();
                //const errorMessages = errorResponse?.map(err => err.description).join('\n') || "An error occurred.";
                //alert("Error: " + errorMessages);
            }
        }
        catch (error) {
            console.error("Error:", error);
            alert("An unexpected error occurred. Please try again later.");
        }
    },

    async createBlogPost() {
        try {
            // Gather data from the form or inputs
            let title = $("#txtTitle").val()?.trim(); // Trim whitespace

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

            // Prepare data object
            let blogPostData = {
                title: title,
                // Include other properties if needed, e.g., createdOn, createdBy, etc.
            };

            // Send data to server
            const response = await fetch('/User/BlogPost/CreateBlogPost', { // Add leading slash for correct URL
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
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
