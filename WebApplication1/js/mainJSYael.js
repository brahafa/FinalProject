
//global 
var numAnswer;
var isGetAnswer = 0;// = false;
var checkAns = 0;


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

//function checkTextField(field) {
//    if (field.value == '') {
//        document.getElementById('MainContent_nextQuestionBtn').style.display = 'none';
//    }
//    else {
//        document.getElementById('MainContent_nextQuestionBtn').style.display = 'inline';
//    }
//}


//display next question on click 
$(document).ready(function () {
    $("#MainContent_nextQuestionBtn").click(function () {
       
        if (checkAns != 0) {

            $.ajax({
                type: "POST",
                url: "ShowQuestionnaire.aspx/updateDbQuesionAsked_click",
                data: '{checkAns: "' + checkAns + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {


                },
                failure: function (response) {
                    alert("ajax failure");

                }
            });
        }
        //hidden nextBtn
        $("#MainContent_nextQuestionBtn").css("display", "none");

        //clear screen from prev question
            if (document.getElementById('MainContent_Americananswer').style.display == 'inline') {

                document.getElementById('MainContent_Americananswer').style.display = 'none';
            }
            if (document.getElementById('MainContent_OpenDiv').style.display == 'inline') {

                document.getElementById('MainContent_OpenDiv').style.display = 'none';
            }

        // result = index1,#,index2,#, index3
        //index1=3 for openQuestion else =2
        //index2= question string
        //index3= all answers
            $.ajax({
                type: "POST",
                url: "ShowQuestionnaire.aspx/displayNextQuestion_click",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    if (result.d == null) {

                        $("#QuestionnaireTitle").text("ברכות! סיימת לענות על השאלון");
                        $("#questionText").empty();

                        document.getElementById('MainContent_nextQuestionBtn').style.display = 'none';



                    }
                    else {

                        var results = result.d.split("#");

                        if (results[0] == '2') {

                            document.getElementById('MainContent_Americananswer').style.display = 'inline';

                            $("#questionText").text(results[1]);
                            //alert(results[1]);

                            numAnswer = parseInt(results[2]);

                            if (numAnswer > 5) {
                                $("#answerText6").text(results[8]);
                                document.getElementById('checkbox6').checked = false;
                            }
                            else {
                                $("#answerText6").empty();
                                $("#checkbox6").css("display", "none");
                            }

                            if (numAnswer > 4) {
                                $("#answerText5").text(results[7]);
                                document.getElementById('checkbox5').checked = false;
                            }
                            else {
                                $("#answerText5").empty();
                                $("#checkbox5").css("display", "none");
                            }

                            if (numAnswer > 3) {
                                $("#answerText4").text(results[6]);
                                document.getElementById('checkbox4').checked = false;
                            }
                            else {
                                $("#answerText4").empty();
                                $("#checkbox4").css("display", "none");
                            }

                            if (numAnswer > 2) {
                                $("#answerText3").text(results[5]);
                                document.getElementById('checkbox3').checked = false;
                            }
                            else {
                                $("#answerText3").empty();
                                $("#checkbox3").css("display", "none");
                            }

                            if (numAnswer > 1) {
                                $("#answerText2").text(results[4]);
                                document.getElementById('checkbox2').checked = false;
                            }
                            else {
                                $("#answerText2").empty();
                                $("#checkbox2").css("display", "none");
                            }

                            if (numAnswer > 0) {
                                $("#answerText1").text(results[3]);
                                document.getElementById('checkbox1').checked = false;
                            }
                            else {
                                $("#answerText1").empty();
                                $("#checkbox1").css("display", "none");
                            }



                        }
                            //open question
                        else if (results[0] == 3) {

                            document.getElementById('MainContent_OpenDiv').style.display = 'inline';
                            $("#questionText").text(results[1]);
                            $("#MainContent_nextQuestionBtn").css("display", "inline");
                            //document.getElementById('MainContent_nextQuestionBtn').style.display = 'none';
                        }


                        //location.reload();

                    }


                },
                failure: function (response) {
                    alert("ajax failure");

                }
            });
        //}//get answer
        //else {

        //    alert("בחר תשובה");
        //}

        

    });
});


function cleanCheckBox1() {

    if (document.getElementById('checkbox1').checked == true) {
        $("#MainContent_nextQuestionBtn").css("display", "inline");
        checkAns = 1;
    }
    else {
        $("#MainContent_nextQuestionBtn").css("display", "none");
        checkAns = 0;
    }

    numAnswer = $('#MainContent_numAns').val();

    //alert(numAnswer);
    if (numAnswer > 5) {
        document.getElementById('checkbox6').checked = false;
    }
    if (numAnswer > 4) {
        document.getElementById('checkbox5').checked = false;
    }
    if (numAnswer > 3) {
        document.getElementById('checkbox4').checked = false;
    }
    if (numAnswer > 2) {
        document.getElementById('checkbox3').checked = false;
    }
    if (numAnswer > 1) {
        document.getElementById('checkbox2').checked = false;
    }    
    
}
function cleanCheckBox2() {
    //check = 2;
    if (document.getElementById('checkbox2').checked == true) {
        $("#MainContent_nextQuestionBtn").css("display", "inline");
        checkAns = 2;
    }
    else {
        $("#MainContent_nextQuestionBtn").css("display", "none");
        checkAns = 0;
    }

    numAnswer = $('#MainContent_numAns').val();

    if (numAnswer > 5) {
        document.getElementById('checkbox6').checked = false;
    }
    if (numAnswer > 4) {
        document.getElementById('checkbox5').checked = false;
    }
    if (numAnswer > 3) {
        document.getElementById('checkbox4').checked = false;
    }
    if (numAnswer > 2) {
        document.getElementById('checkbox3').checked = false;
    }
    if (numAnswer > 0) {
        document.getElementById('checkbox1').checked = false;
    }
}
function cleanCheckBox3() {
    //check = 3;
    if (document.getElementById('checkbox3').checked == true) {
        $("#MainContent_nextQuestionBtn").css("display", "inline");
        checkAns = 3;
    }
    else {
        $("#MainContent_nextQuestionBtn").css("display", "none");
        checkAns = 0;
    }

    numAnswer = $('#MainContent_numAns').val();

    if (numAnswer > 5) {
        document.getElementById('checkbox6').checked = false;
    }
    if (numAnswer > 4) {
        document.getElementById('checkbox5').checked = false;
    }
    if (numAnswer > 3) {
        document.getElementById('checkbox4').checked = false;
    }
    if (numAnswer > 1) {
        document.getElementById('checkbox2').checked = false;
    }
    if (numAnswer > 0) {
        document.getElementById('checkbox1').checked = false;
    }
}
function cleanCheckBox4() {
    //check = 4;
    if (document.getElementById('checkbox4').checked == true) {
        $("#MainContent_nextQuestionBtn").css("display", "inline");
        checkAns = 4;
    }
    else {
        $("#MainContent_nextQuestionBtn").css("display", "none");
        checkAns = 0;
    }

    numAnswer = $('#MainContent_numAns').val();

    if (numAnswer > 5) {
        document.getElementById('checkbox6').checked = false;
    }
    if (numAnswer > 4) {
        document.getElementById('checkbox5').checked = false;
    }
    if (numAnswer > 2) {
        document.getElementById('checkbox3').checked = false;
    }
    if (numAnswer > 1) {
        document.getElementById('checkbox2').checked = false;
    }
    if (numAnswer > 0) {
        document.getElementById('checkbox1').checked = false;
    }
}
function cleanCheckBox5() {
    //check = 5;
    if (document.getElementById('checkbox5').checked == true) {
        $("#MainContent_nextQuestionBtn").css("display", "inline");
        checkAns = 5;
    }
    else {
        $("#MainContent_nextQuestionBtn").css("display", "none");
        checkAns = 0;
    }

    numAnswer = $('#MainContent_numAns').val();

    if (numAnswer > 5) {
        document.getElementById('checkbox6').checked = false;
    }
    if (numAnswer > 3) {
        document.getElementById('checkbox4').checked = false;
    }
    if (numAnswer > 2) {
        document.getElementById('checkbox3').checked = false;
    }
    if (numAnswer > 1) {
        document.getElementById('checkbox2').checked = false;
    }
    if (numAnswer > 0) {
        document.getElementById('checkbox1').checked = false;
    }
}
function cleanCheckBox6() {
    //check = 6;
    if (document.getElementById('checkbox6').checked == true) {
        $("#MainContent_nextQuestionBtn").css("display", "inline");
        checkAns = 6;
    }
    else {
        $("#MainContent_nextQuestionBtn").css("display", "none");
        checkAns = 0;
    }

    numAnswer = $('#MainContent_numAns').val();

    if (numAnswer > 4) {
        document.getElementById('checkbox5').checked = false;
    }
    if (numAnswer > 3) {
        document.getElementById('checkbox4').checked = false;
    }
    if (numAnswer > 2) {
        document.getElementById('checkbox3').checked = false;
    }
    if (numAnswer > 1) {
        document.getElementById('checkbox2').checked = false;
    }
    if (numAnswer > 0) {
        document.getElementById('checkbox1').checked = false;
    }
}

//addRemove course click in home page 
$(document).ready(function () {
    $("#MainContent_addRemoveBtn").click(function () {

        var courseInput = $("#MainContent_courseName").val();
        var userType = $('#MainContent_sessionInput').val();
        //alert(userType);
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
                    if (userType === "0")
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
                    else if (userType === "1")//student
                    {

                        $.ajax({
                            type: "POST",
                            url: "HomePageStudent.aspx/removeCourse_click",
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
                else// dont want remove course
                {
                    location.reload();
                }
            }
            else // add new course
            {
                if (userType === "0")
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
                else if(userType === "1")
                {
                    $.ajax({
                        type: "POST",
                        url: "HomePageStudent.aspx/addCourse_click",
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
        }


    });
});

// display input to inser name new course
$(document).ready(function () {
    $("#MainContent_addCourseBtn").click(function () {

        document.getElementById('buttonAddRemove').style.display = 'none';
        document.getElementById('inputAddRemove').style.display = 'inline';

        document.getElementById('MainContent_addRemoveBtn').value = "הוסף";
        //document.getElementById('MainContent_addRemoveBtnStudent').value = "הוסף";
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
        //document.getElementById('MainContent_addRemoveBtnStudent').value = "הסר";
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
        //document.getElementById('MainContent_addCourseBtnStudent').style.display = 'inline';
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