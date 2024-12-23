let user = {
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
