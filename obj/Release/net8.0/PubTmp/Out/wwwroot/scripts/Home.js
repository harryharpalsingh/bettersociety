let home = {
    async getQAList() {
        try {
            //let p = {
            //    AttendanceID: attendanceId,
            //    ServerType: _serverType
            //};

            //let str = { 'prm': p };

            home.toggleSkeleton(1);

            // /AreaName/ControllerName/ActionName
            const response = await fetch('/Home/GetQA', {
                method: "GET",
                headers: { 'Content-Type': 'application/json' },
                //body: JSON.stringify(str),
            });

            const result = await response.json();
            debugger
            if (result && result.status === 1) {
                const tbl = result.questions;

                let qa = ``;
                tbl.forEach(t => {
                    qa += `
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
                            <h5 class="mb-2 fw-medium"> <a asp-controller="Home" asp-action="QuestionDetail" data-id="${t.id}">${t.title}</a> </h5>
                            <p class="mb-2 truncate lh-20 fs-15">
                            ${t.answers[0].answer}  
                            </p>
                            <div class="tags">
                                <a href="#" class="tag-link">javascript</a>
                                <a href="#" class="tag-link">bootstrap-4</a>
                                <a href="#" class="tag-link">jquery</a>
                                <a href="#" class="tag-link">select</a>
                            </div>
                            <div class="media media-card user-media align-items-center px-0 border-bottom-0 pb-0">
                                <a href="user-profile.html" class="media-img d-block">
                                    <img src="../assets/images/user-square.svg" alt="avatar">
                                </a>
                                <div class="media-body d-flex flex-wrap align-items-center justify-content-between">
                                    <div>
                                        <h5 class="pb-1"><a href="user-profile.html">Arden Smith</a></h5>
                                        <div class="stats fs-12 d-flex align-items-center lh-18">
                                            <span class="text-black pe-2" title="Reputation score">224</span>
                                            <span class="pe-2 d-inline-flex align-items-center" title="Gold badge"><span class="ball gold"></span>16</span>
                                            <span class="pe-2 d-inline-flex align-items-center" title="Silver badge"><span class="ball silver"></span>93</span>
                                            <span class="pe-2 d-inline-flex align-items-center" title="Bronze badge"><span class="ball"></span>136</span>
                                        </div>
                                    </div>
                                    <small class="meta d-block text-end">
                                        <span class="text-black d-block lh-18">asked</span>
                                        <span class="d-block lh-18 fs-12">6 hours ago</span>
                                    </small>
                                </div>
                            </div>
                        </div>
                    </div>`;
                });

                $('#div-question-answers').empty().append(qa);

                home.toggleSkeleton(0);
            }
        }
        catch (error) {
            console.log(error);
        }
    },

    toggleSkeleton(prm) {
        switch (prm) {
            case 1:
                $('#div-home-skeleton').show();
                break;
            case 0:
                $('#div-home-skeleton').hide();
                break;
        }
    }
};