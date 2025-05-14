let auth = {
    async signUp() {
        $('#btnSignup').prop('disabled', true);

        let userFullName = $('#txtFullName').val()?.trim();
        var fullNamePattern = /^[A-Z][a-zA-Z' -]{1,49}$/;
        let userName = $('#txtUserName').val()?.trim(); // Trim whitespace
        let email = $('#txtUserEmail').val();
        let password = $('#txtUserPassword').val();

        /*
        Regex pattern checks if the name:
            Starts with an uppercase letter.
            Only contains letters, spaces, apostrophes, or hyphens.
            Is between 2 and 50 characters long.
        */

        // Check if the full name matches the pattern
        if (!fullNamePattern.test(userFullName)) {
            // Show error message if validation fails
            global.toggleAlert(2, "Please enter a valid Full name.", "#txtUserName");
            return false; // Prevent form submission
        }

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
            FullName: userFullName,
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
                global.toggleAlert(0, "Registration successful! Redirecting to Login page..", "");

                setTimeout(function () {
                    window.open("/Login", "_self");
                }, 5000); // Delay of 5 seconds (5000 milliseconds)
            }
            else if (response.status === 400) {
                // 400 Bad Request (Validation errors or other issues)
                let errorResponse;

                try {
                    // Try to parse the response as JSON
                    errorResponse = await response.json();
                    console.log('Parsed JSON response:', errorResponse); // Log the parsed response for debugging
                }
                catch (error) {
                    // Fallback for non-JSON responses
                    try {
                        const textResponse = await response.text();
                        console.log('Parsed text response:', textResponse); // Log the text response for debugging
                        errorResponse = { message: textResponse };
                    }
                    catch (textError) {
                        // Handle case when even text parsing fails
                        console.error('Error parsing response as text:', textError);
                        errorResponse = { message: "An unknown error occurred." };
                    }
                }

                // Check for errors field (case-insensitive) or fallback to message
                const errorMessages = (errorResponse?.errors || errorResponse?.Errors)?.length
                    ? (errorResponse.errors || errorResponse.Errors).join('\n')  // Join the validation errors if they exist
                    : errorResponse?.message || "Invalid request."; // Fallback if no errors or message found

                // Display the error messages using the global alert function
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
                window.open("/u", "_self");

            }
            else {
                const errorResponse = await response.json();
                global.toggleAlert(1, errorResponse.message, "");
                return;
            }
        }
        catch (error) {
            console.error("Error:", error);
            global.toggleAlert(1, "An unexpected error occurred. Please try again later.", "");
            return;
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

let question = {
    //not in use
    async getTags() {
        $('#divTagList').empty();
        try {
            const response = await fetch('/u/Tags/GetAllTags', {
                method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                },
                //body: JSON.stringify({ "blogPostData": blogPostData, "tagIds": tagIds }),
            });

            // Handle the response
            if (response.ok) {
                const result = await response.json();

                let _tagList = result.tags;
                let _tagListHtml = ``;

                if (_tagList && _tagList.length > 0) {
                    _tagList.forEach(t => {
                        _tagListHtml += `<div id='divTag_${t.id}' class='question-tag'>${t.tagName}</div>`;
                    });

                    if (_tagListHtml != '') {
                        $('#divTagList').append(_tagListHtml);
                    }
                }
            }
            else if (response.status = 404) {
                global.toggleAlert(1, "No Tags found!", "");
            }
            else {
                try {
                    const errorResponse = await response.json();
                    global.toggleAlert(1, "A wild error appears : " + JSON.stringify(errorResponse), "");
                }
                catch {
                    alert("An unknown error occurred.");
                }
            }
        }
        catch (e) {
            console.error("An error occurred:", e);
            alert("An error occurred while saving the blog post. Please try again.");
        }
    },

    //not in use
    filterTags() {
        let searchText = $("#txtTags").val().toLowerCase();
        let tags = $("#divTagList .question-tag");

        if (searchText.length === 0) {
            $("#divTagList").hide(); // Hide if input is empty
            return;
        }

        let anyMatch = false;
        tags.each(function () {
            let tagText = $(this).text().toLowerCase();
            if (tagText.includes(searchText)) {
                $(this).show();
                anyMatch = true;
            }
            else {
                $(this).hide();
            }
        });

        if (anyMatch) {
            $("#divTagList").show();
        }
        else {
            $("#divTagList").hide();
        }
    },

    async getCategories() {
        $('#ddlCategory').empty().append(`<option selected value="0">Select a Category</option>`);
        try {
            const response = await fetch('/u/ask-question/GetCategories', {
                method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                },
                //body: JSON.stringify({ "blogPostData": blogPostData, "tagIds": tagIds }),
            });

            // Handle the response
            if (response.ok) {
                const result = await response.json();
                let categoryList = ``;

                if (result.categories && result.categories.length > 0) {
                    result.categories.forEach(c => {
                        categoryList += `<option value="${c.id}">${c.category}</option>`;
                    });

                    if (categoryList != '') {
                        $('#ddlCategory').append(categoryList);
                    }
                }
            }
            else if (response.status = 404) {
                global.toggleAlert(1, "No Category found!", "");
            }
            else {
                try {
                    const errorResponse = await response.json();
                    global.toggleAlert(1, "A wild error appears : " + JSON.stringify(errorResponse), "");
                }
                catch {
                    global.toggleAlert(1, "An unknown error occurred", "");
                }
            }
        }
        catch (e) {
            global.toggleAlert(1, "A wild error appears : " + e, "");
        }
    },

    async postQuestion() {
        try {
            // Gather data from the form or inputs
            let title = $("#txtQuestionTitle").val()?.trim();
            let tags = $('#txtTags').val()?.trim();
            let tagArray = [];
            let postDetail = $(".jqte_editor").html()?.trim();
            let category = $('#ddlCategory :selected').val();

            // Validate Title
            if (!title) {
                global.toggleAlert(1, "Question title is required and cannot be empty.", "#txtQuestionTitle");
                return;
            }

            // Additional Title Validation: Minimum and Maximum Length
            if (title.length < 5 || title.length > 100) {
                global.toggleAlert(2, "Title must be between 5 and 100 characters.", "#txtQuestionTitle");
                return;
            }

            if (tags != '') {
                tagArray = tags ? tags.split(',').map(tag => tag.trim()) : [];
            }

            // Validate Post detail
            if (!postDetail) {
                global.toggleAlert(2, "Post Content is required and cannot be empty.", "");
                return;
            }

            if (category == '' || category == 0) {
                global.toggleAlert(1, "Please select category.", "");
                return;
            }

            // Prepare data object
            let askQuestionData = {
                title: title,
                QuestionDetail: postDetail,
                CategoryId: category
                // Include other properties if needed, e.g., createdOn, createdBy, etc.
            };

            // Send data to server
            const response = await fetch('/u/ask-question/CreatePost', {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                    //'X-XSRF-TOKEN': auth.getCookie("XSRF-TOKEN")
                },
                body: JSON.stringify({
                    "questionData": askQuestionData,
                    "tagNames": tagArray
                }),
            });

            // Handle the response
            if (response.ok) {
                const result = await response.json();
                global.toggleAlert(0, "Question posted successfully.", "");
                //console.log(result);
            }
            else {
                const errorResponse = await response.json();
                global.toggleAlert(1, `An unexpected error occurred : ${JSON.stringify(errorResponse)}`, "");
            }
        }
        catch (err) {
            global.toggleAlert(1, `An unexpected error occurred : ${err}`, "");
        }
    },

    async postQuestionWithAttachment() {
        try {
            let title = $("#txtQuestionTitle").val()?.trim();
            let tags = $('#txtTags').val()?.trim();
            //let tagArray = [];
            let postDetail = $(".jqte_editor").html()?.trim();
            let category = $('#ddlCategory :selected').val();
            let fileInput = $('#fileAttachment')[0]?.files[0];

            if (!title || title.length < 5 || title.length > 100) {
                global.toggleAlert(2, "Title must be between 5 and 100 characters.", "#txtQuestionTitle");
                return;
            }

            if (!postDetail) {
                global.toggleAlert(2, "Post Content is required and cannot be empty.", "");
                return;
            }

            if (category == '' || category == 0) {
                global.toggleAlert(1, "Please select category.", "");
                return;
            }

            // File type validation
            const allowedExtensions = ['jpg', 'jpeg', 'png', 'gif', 'webp'];
            if (fileInput) {
                const fileExtension = fileInput.name.split('.').pop().toLowerCase();
                if (!allowedExtensions.includes(fileExtension)) {
                    global.toggleAlert(2, "Invalid file type.  Allowed types: .jpg,  .jpeg,  .png,  .gif,  .webp", "#fileAttachment");
                    return;
                }

                // File size validation (Optional - Recommended)

                /* fileInput.size → This property represents the size of the selected file in bytes.
                   maxSizeInMB → This is your desired maximum file size limit (in MB).
                   1024 * 1024 → This converts 1 MB into bytes since fileInput.size is measured in bytes.
                */

                const maxSizeInMB = 5; // Maximum 5 MB
                // if (fileInput.size > 5,242,880 bytes)
                if (fileInput.size > maxSizeInMB * 1024 * 1024) {
                    global.toggleAlert(2, `File size exceeds ${maxSizeInMB} MB limit.`, "#fileAttachment");
                    return;
                }
            }

            let askQuestionData = {
                title: title,
                QuestionDetail: postDetail,
                CategoryId: category
                // Include other properties if needed, e.g., createdOn, createdBy, etc.
            };

            // Prepare form data for file upload
            let formData = new FormData();
            formData.append('QuestionData', JSON.stringify(askQuestionData));
            if (tags) {
                //tagArray = tags.split(',').map(tag => tag.trim());
                tags.split(',').forEach(tag => {
                    formData.append('TagNames', tag.trim()); // Directly appending trimmed tags
                });
            }

            //formData.append('title', title);
            //formData.append('CategoryId', category);
            //formData.append('tagNames', JSON.stringify(tagArray));

            // Append file if present
            if (fileInput) {
                formData.append('fileAttachment', fileInput);
            }

            const response = await fetch('/u/ask-question/CreatePostWithAttachment', {
                method: "POST",
                body: formData
            });

            if (response.ok) {
                const result = await response.json();
                global.toggleAlert(0, "Question posted successfully.", "");
                console.log(result);
            }
            else {
                const errorResponse = await response.json();
                alert("Error: " + JSON.stringify(errorResponse));
            }
        } catch (e) {
            console.error("An error occurred:", e);
            alert("An error occurred while saving the blog post. Please try again.");
        }
    },
};

let posts = {
    async getAllPosts() {
        try {
            // /AreaName/ControllerName/ActionName
            const response = await fetch('/Home/GetQA', {
                method: "GET",
                headers: { 'Content-Type': 'application/json' },
                //body: JSON.stringify(str),
            });

            const result = await response.json();
            //debugger
            if (result && result.status === 1) {
                const posts = result.questions;

                let qaContent = ``;
                posts.forEach(post => {
                    //const _answer = post.answers && post.answers.length > 0 ? post.answers[0].answer : "No answers available yet.";

                    let tagsHtml = '<div class="better-tags-container">';
                    post.tags.forEach(function (tag) {
                        tagsHtml += `<div class="tag-link">${tag}</div>`;
                    });
                    tagsHtml += '</div>';

                    qaContent += `
                    <div class="media media-card rounded-0 shadow-none mb-0 bg-transparent p-3 border-bottom border-bottom-gray">
                        <div class="votes text-center votes-2">
                            <div class="vote-block">
                                <span class="vote-counts d-block text-center pr-0 lh-20 fw-medium">3</span>
                                <span class="vote-text d-block fs-13 lh-18">votes</span>
                            </div>
                            <div class="answer-block answered-accepted my-2">
                                <span class="answer-counts d-block lh-20 fw-medium">3</span>
                                <span class="answer-text d-block fs-13 lh-18">answers</span>
                            </div>
                            <div class="view-block">
                                <span class="view-counts d-block lh-20 fw-medium">12</span>
                                <span class="view-text d-block fs-13 lh-18">views</span>
                            </div>
                        </div>
                        <div class="media-body">
                            <h5 class="mb-2 better-post-title"> <a href='u/all-post/${post.slug}' class='cursor-pointer' data-id="${post.id}">${post.title}</a> </h5>
                            <p class="mb-2 truncate lh-20 fs-15">
                            ${post.questionDetail}  
                            </p>
                            ${tagsHtml}
                            <div class="media media-card user-media align-items-center px-0 border-bottom-0 pb-0">
                                <a href="user-profile.html" class="media-img d-block">
                                    <img src="../assets/images/user-square.svg" alt="avatar">
                                </a>
                                <div class="media-body d-flex flex-wrap align-items-center justify-content-between">
                                    <div>
                                        <h5 class="pb-1"><a href="user-profile.html">${post.createdByUserFullname}</a></h5>
                                        <!--<div class="stats fs-12 d-flex align-items-center lh-18">
                                            <span class="text-black pe-2" title="Reputation score">224</span>
                                            <span class="pe-2 d-inline-flex align-items-center" title="Gold badge"><span class="ball gold"></span>16</span>
                                            <span class="pe-2 d-inline-flex align-items-center" title="Silver badge"><span class="ball silver"></span>93</span>
                                            <span class="pe-2 d-inline-flex align-items-center" title="Bronze badge"><span class="ball"></span>136</span>
                                        </div>-->
                                    </div>
                                    <small class="meta d-block text-end">
                                        <span class="text-black d-block lh-18">${post.categoryName}</span>
                                        <span class="d-block lh-18 fs-12">${post.createdAgo}</span>
                                    </small>
                                </div>
                            </div>
                        </div>
                    </div>`;
                });

                $('#div-question-answers').empty().append(qaContent);

                global.toggleUserHomeSkeleton(0);
            }
        }
        catch (error) {
            console.log(error);
        }
    },

    async getPost(slug) {
        try {
            let postDto = { Slug: slug };

            // /AreaName/ControllerName/ActionName
            const response = await fetch('/u/all-post/get-post-detail-by-slug', {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(postDto),
            });

            if (response.ok) {
                const result = await response.json();

                let tagsHtml = '';
                result.tags.forEach(function (tag) {
                    tagsHtml += `<div class="tag-link tag-link-md">${tag}</div>`;
                });

                $('#spnPostTitle').empty().append(result.title);
                $('#spnPostedByUserFullname').empty().append(result.createdByUserFullname);
                $('#spnPostedOn').empty().append(result.createdAgo);
                $('#divPostContent').empty().append(result.questionDetailFull);
                $('#divPostRelatedTags').empty().append(tagsHtml);
                //console.log(response);
            }
        }
        catch (error) {
            console.log(error);
        }
    },
};

let global = {
    async executeOrder(button, asyncFunction, ...args) {

        if (button && button.preventDefault) {
            button.preventDefault(); // Prevent default action only if button is an event object
        }

        try {
            global.toggleLoader(1);
            await asyncFunction(...args);
            global.toggleLoader(0);

            //#region how to use ...args
            /* function exampleFunction(...args) {
                console.log(args); // This will log all the arguments passed to the function in an array.
            }
            exampleFunction(1, 2, 3, "hello", true); */
            //#endregion
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

    toggleUserHomeSkeleton(prm) {
        switch (prm) {
            case 1:
                $('#div-user-home-skeleton').show();
                break;
            case 0:
                $('#div-user-home-skeleton').hide();
                break;
        }
    }
};