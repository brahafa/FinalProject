


function onClickQuestionnaire() {

    setQuestionnaireID();

    document.getElementById('MainContent_stockQuestionnaire').style.display = 'none';
    document.getElementById('MainContent_StockQuestion').style.display = 'inline';

    alert("click");

}

//function onClickQuestion() {
//    document.getElementById('left_column_Body_StockQuestion').style.display = 'none';
//    document.getElementById('left_column_Body_fade').style.display = 'none';

//}

//save name of Questionnaire
function setQuestionnaireName(nameAndId) {

    var nameAndIdArry = nameAndId.split(",");
    $("#MainContent_QuestionnaireName").val(nameAndIdArry[0]);
    $("#MainContent_idQuestnaire").val(nameAndIdArry[1].trim());

}

function setQuestionId(id) {
    $("#MainContent_QuestionId").val(id);
    alert(id);
}


//addRemove course click
$(document).ready(function () {
    $("#MainContent_addRemoveBtn").click(function () {

        var courseInput = $("#MainContent_courseName").val();

        if (courseInput == "") {

            document.getElementById('MainContent_errMesegeEmpty').style.display = 'inline';
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
                        data: '{courseInput: "' + courseInput + '"}',
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
                    data: '{courseInput: "' + courseInput + '"}',
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
        //document.getElementById('<%=MainContent_addRemoveBtn.ClientID%>').value = "הוסף";
        $("#MainContent_addOrremove").val("הוסף");



    });
});

// display input to inser name to remove course
$(document).ready(function () {
    $("#MainContent_removeCourseBtn").click(function () {

        document.getElementById('buttonAddRemove').style.display = 'none';
        document.getElementById('inputAddRemove').style.display = 'inline';

        document.getElementById('MainContent_addRemoveBtn').value = "הסר";
        //document.getElementById('<%=MainContent_addRemoveBtn.ClientID%>').value = "הסר";
        $("#MainContent_addOrremove").val("הסר");

    });
});


// remove course FromQ
$(document).ready(function () {
    $("#MainContent_removeCourseBtnFromQ").click(function () {

        var remove = confirm("האם אתה בטוח שברצונך להסיר את הקורס כולל השאלונים?");
        if (remove) {
            $.ajax({
                type: "POST",
                url: "StockQuestionnaires.aspx/removeCourse",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    window.location.replace("HomePage.aspx");
                },
                failure: function (response) {
                    alert("ajax failure");

                }
            });
        }
    });
});

// close button
$(document).ready(function () {
    $("#MainContent_closeButton").click(function () {

        document.getElementById('MainContent_removeCourseBtn').style.display = 'inline';
        document.getElementById('MainContent_addCourseBtn').style.display = 'inline';
        document.getElementById('buttonAddRemove').style.display = 'inline';


        document.getElementById('MainContent_errMesegeEmpty').style.display = 'none';
        document.getElementById('inputAddRemove').style.display = 'none';


    });
});

// close button Question
$(document).ready(function () {
    $("#MainContent_closeButtonQuestions").click(function () {

        document.getElementById('MainContent_StockQuestion').style.display = 'none';
        document.getElementById('MainContent_stockQuestionnaire').style.display = 'inline';
        document.getElementById('MainContent_removeQuestionnaireBtn').style.display = 'none';
        document.getElementById('MainContent_selectCourse').style.display = 'none';

    });
});

// close button remove Questionnaire
$(document).ready(function () {
    $("#MainContent_closeButtonRemoveQ").click(function () {

        document.getElementById('inputQtoRemove').style.display = 'none';

    });
});

// remove Questionnaire
$(document).ready(function () {
    $("#MainContent_removeQuestionnaireBtn").click(function () {

        var remove = confirm("האם אתה בטוח שברצונך להסיר את השאלון כולל כל הסטטיסטיקות?");
        if (remove) {
            $.ajax({
                type: "POST",
                url: "StockQuestionnaires.aspx/removeQuestionnaire",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (idCourse) {

                    window.location.replace("StockQuestionnaires.aspx?IdCourse=" + idCourse.d);
                },
                failure: function (response) {
                    alert("ajax failure");

                }
            });
        }
    });
});



// copy Questionnaire
$(document).ready(function () {
    $("#MainContent_copyQuestionnaireBtn").click(function () {


        selectCourse = $("#MainContent_selectCourse").val();
        if (selectCourse != "-1") {

            var idQ = $("#MainContent_idQuestnaire").val();
            var SelectValue = idQ + "," + selectCourse;

            $.ajax({
                type: "POST",
                url: "StockQuestionnaires.aspx/copyQuestionnaire",
                data: '{SelectValue: "' + SelectValue + '" }',// idCourse 
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (idCourse) {

                    window.location.replace("StockQuestionnaires.aspx?IdCourse=" + idCourse.d);
                },
                failure: function (response) {
                    alert("ajax failure");

                }
            });
        }
        else {
            alert("בחר תחילה קורס אליו ברצונך להעתיק את השאלון.");
        }
    });
});

// show Questionnaire
$(document).ready(function () {
    $("#MainContent_classDisplayBtn").click(function () {


        window.location.replace("ShowQuestionnaire.aspx");
    });
});