
function onClickQuestionnaire() {

    setQuestionnaireID();

    document.getElementById('MainContent_stockQuestionnaire').style.display = 'none';
    document.getElementById('MainContent_StockQuestion').style.display = 'inline';

    alert("click");

}

function onClickQuestion() {
    document.getElementById('left_column_Body_StockQuestion').style.display = 'none';
    document.getElementById('left_column_Body_fade').style.display = 'none';

}

//function closeStockQuestion() {

//}

function setQuestionnaireID(id) {
    $("#MainContent_QuestionnaireId").val(id);
    // alert("click");
}

function setQuestionID(id) {
    $("#MainContent_QuestionId").val(id);
}


//addRemove course click
$(document).ready(function () {
    $("#MainContent_addRemoveBtn").click(function () {

        var courseName = $("#MainContent_courseName").val();

        if (courseName == "") {

            alert('הכנס שם/קוד קורס');
        }
        else//input not empty
        {
            if ($("#MainContent_addRemoveBtn").val() == "הסר")// remove course
            {

                var isRemove = confirm("אתה בטוח שברצונך להסיר את הקורס?");

                if (isRemove)// want remove course
                {

                    $.ajax({
                        type: "POST",
                        url: "HomePage.aspx/removeCourse_click",
                        data: '{courseName: "' + courseName + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            alert(result.d);// display string to client an explanation of what happened
                            location.reload();

                        },
                        failure: function (response) {
                            alert("ajax failure");

                        }
                    });


                }
                else// dont want remove course
                {
                    location.reload();
                }
            }
            else // add new course
            {
                $.ajax({
                    type: "POST",
                    url: "HomePage.aspx/addCourse_click",
                    data: '{courseName: "' + courseName + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {

                        alert(result.d);// display string to client an explanation of what happened
                        location.reload();
                    },
                    failure: function (response) {
                        alert("ajax failure");

                    }
                });


            }
        }


    });
});


// display input to inser name new course
$(document).ready(function () {
    $("#MainContent_addCourseBtn").click(function () {

        document.getElementById('buttonAddRemove').style.display = 'none';
        document.getElementById('inputAddRemove').style.display = 'inline';

        document.getElementById('MainContent_addRemoveBtn').value = "הוסף";
        $("#MainContent_addOrremove").val("הוסף");



    });
});

// remove course
$(document).ready(function () {
    $("#MainContent_removeCourseBtn").click(function () {

        document.getElementById('buttonAddRemove').style.display = 'none';
        document.getElementById('inputAddRemove').style.display = 'inline';

        document.getElementById('MainContent_addRemoveBtn').value = "הסר";
        $("#MainContent_addOrremove").val("הסר");

    });
});

// close button
$(document).ready(function () {
    $("#MainContent_closeButton").click(function () {

        document.getElementById('buttonAddRemove').style.display = 'inline';
        document.getElementById('inputAddRemove').style.display = 'none';

    });
});
