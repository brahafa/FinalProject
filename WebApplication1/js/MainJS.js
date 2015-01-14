var typeQuestion=1;
var check = 0;
var checkYesNo = 0;
var StrAns='';
var numAns = 0;
var select_Course = ''; //the value(id) of course selected
var QuestionnaireSelect = ''; //the value(id) of  Questionnair selected
// display input to inser name new course
$(document).ready(function () {
    $("#MainContent_addCourseBtn").click(function () {

        document.getElementById('MainContent_removeCourseBtn').style.display = 'none';
        document.getElementById('MainContent_addCourseBtn').style.display = 'none';
        document.getElementById('MainContent_courseName').style.display = 'inline';
        document.getElementById('MainContent_addRemoveBtn').style.display = 'inline';
        document.getElementById('MainContent_addRemoveBtn').value = "הוסף";
        $("#MainContent_addOrremove").val("הוסף");
       


    });
});

// remove course
$(document).ready(function () {
    $("#MainContent_removeCourseBtn").click(function () {

        document.getElementById('MainContent_removeCourseBtn').style.display = 'none';
        document.getElementById('MainContent_addCourseBtn').style.display = 'none';
        document.getElementById('MainContent_courseName').style.display = 'inline';
        document.getElementById('MainContent_addRemoveBtn').style.display = 'inline';
        document.getElementById('MainContent_addRemoveBtn').value = "הסר";
        $("#MainContent_addOrremove").val("הסר");
        
      


    });
});


//save course id clicked
function setCourseID(id) {
    $("#MainContent_courseId").val(id);
}


function UserIn() {
    var elem;

    elem = document.getElementById("existUser");
    elem.parentNode.removeChild(elem);

    elem = document.getElementById("conectedUser");
    elem.parentNode.appendChild(elem);
}

/////////////////register--choose type user
$(document).ready(function () {
    $("#MainContent_selected_Type").change(function () {
        if ($("#MainContent_selected_Type").val() == '0') {// type = lecture
            document.getElementById('MainContent_selected_Degree').style.display = 'inline';
        }
        else if ($("#MainContent_selected_Type").val() != '0') {
            document.getElementById('MainContent_selected_Degree').style.display = 'none';
        }
    });
});

//////////display image in register page////////
function previewFile() {
    var preview = document.querySelector("#MainContent_Avatar");
    var file = document.querySelector("#MainContent_avatarUpload").files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}
//select questionnires/////
function SelectQuestionnaires(){
    QuestionnaireSelect = $("#MainContent_selected_Questionnaires").val();
    if (QuestionnaireSelect == -2) {
        document.getElementById('Newquestionn').style.display = 'inline';
        document.getElementById('MainContent_select_permit').style.display = 'inline';
    }
    else {
        document.getElementById('Newquestionn').style.display = 'none';
        document.getElementById('MainContent_select_permit').style.display = 'none';

    }
}




//מקבל מפונקציה של סי שארפ מחרוזת שבה יש את כל השאלונים של הקורס שניבחר
//המחרוזת נשלחת לשיטה הבאה שבה מאותחל הסלקט החדש של השאלונים
/////////Add questionnires////////////
function SelectCurse() {
    document.getElementById('Newquestionn').style.display = 'none';
    document.getElementById('MainContent_select_permit').style.display = 'none';

    select_Course = $("#MainContent_select_Course").val();
    if (select_Course != "-1") {
        $.ajax({
            type: "POST",
            url: "AddQuestion.aspx/updateSelectQuestionnaires",
            data: '{SelectValue: "' + select_Course + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var tmp = data.d;
                //create option of  questionnaires select
                initQuestionnaires(tmp);      
            },
            failure: function (response) {
            }
        });
    }
    else {
        var select = document.getElementById('MainContent_selected_Questionnaires');
        var length1 = select.options.length;
        //clear the questionnaires select 
        for (var i = length1; i > 0; i--) {
            select.remove(i);
        }
    }
}
//init the Questionnaires select by cours id
//איתחול הסלקט של השאלונים ומחיקתם לפני תחילת המילוי כדי שלא יהיה כפל שאלונים
function initQuestionnaires(tmp){
    var select = document.getElementById('MainContent_selected_Questionnaires');
    var words = tmp.split(",");
    var length1 = select.options.length;
    //clear the questionnaires select 
    for (var i = length1; i >0; i--) {
        select.remove(i);
    }
    //create option of  questionnaires select 
    for (var i = 0; i < words.length-1; i++)
    {
        var opt = document.createElement('option');
        opt.value = words[i];
        opt.innerHTML = words[i + 1];
        i++;
        select.appendChild(opt);
    }
    var opt = document.createElement('option');
    opt.value =-2;
    opt.innerHTML = "צור שאלון";
    select.appendChild(opt);
}

///display or hide the answer divs
//כל אחת מהשיטות הבאות שומרת את סוג השאלה הנוכחית של המשתמש
//ומציגה רק את הדיב המבוקש, למשל רק שאלות כן לא או רק ריבוי תשובות
$(document).ready(function () {
    $("#MainContent_openQuestionBtn").click(function () {
        typeQuestion = 3;
        check = 0;
        checkYesNo = 0;

        document.getElementById('Americananswer').style.display = 'none';
        document.getElementById('OpenDiv').style.display = 'inline';
        document.getElementById('yesNoDiv').style.display = 'none';

        document.getElementById('CheckYes').checked = false;
        document.getElementById('CheckNo').checked = false;

        document.getElementById('answer1').value = '';
        document.getElementById('answer2').value = '';
        document.getElementById('answer3').value = '';
        document.getElementById('answer4').value = '';
        document.getElementById('answer5').value = '';
        document.getElementById('answer6').value = '';

        document.getElementById('check1').checked = false;
        document.getElementById('check2').checked = false;
        document.getElementById('check3').checked = false;
        document.getElementById('check4').checked = false;
        document.getElementById('check5').checked = false;
        document.getElementById('check6').checked = false;

        document.getElementById('MainContent_openAnswerID').value = '';



    });
});

$(document).ready(function () {
    $("#MainContent_americanQuestionBtn").click(function () {
        typeQuestion = 1;
        checkYesNo = 0
        document.getElementById('Americananswer').style.display = 'inline';
        document.getElementById('OpenDiv').style.display = 'none';
        document.getElementById('yesNoDiv').style.display = 'none';


        document.getElementById('CheckYes').checked = false;
        document.getElementById('CheckNo').checked = false;

        document.getElementById('MainContent_openAnswerID').value = '';

    });
});

$(document).ready(function () {
    $("#MainContent_yesNoQuestionBtn").click(function () {
        typeQuestion = 2;
        check = 0;
        document.getElementById('Americananswer').style.display = 'none';
        document.getElementById('OpenDiv').style.display = 'none';
        document.getElementById('yesNoDiv').style.display = 'inline';
       
        document.getElementById('answer1').value = '';
        document.getElementById('answer2').value = '';
        document.getElementById('answer3').value = '';
        document.getElementById('answer4').value = '';
        document.getElementById('answer5').value = '';
        document.getElementById('answer6').value = '';

        document.getElementById('check1').checked = false;
        document.getElementById('check2').checked = false;
        document.getElementById('check3').checked = false;
        document.getElementById('check4').checked = false;
        document.getElementById('check5').checked = false;
        document.getElementById('check6').checked = false;

        document.getElementById('MainContent_openAnswerID').value = '';

    });
});
//בבחירת כל אחד מהצ'קבוקסים מתבטלת כל בחירה קיימת אחרת
function cleanCheck1() {
    check = 1;
    document.getElementById('check2').checked = false;
    document.getElementById('check3').checked = false;
    document.getElementById('check4').checked = false;
    document.getElementById('check5').checked = false;
    document.getElementById('check6').checked = false;
}
function cleanCheck2() {
    check = 2;
    document.getElementById('check1').checked = false;
    document.getElementById('check3').checked = false;
    document.getElementById('check4').checked = false;
    document.getElementById('check5').checked = false;
    document.getElementById('check6').checked = false;
}
function cleanCheck3() {
    check = 3;
    document.getElementById('check2').checked = false;
    document.getElementById('check1').checked = false;
    document.getElementById('check4').checked = false;
    document.getElementById('check5').checked = false;
    document.getElementById('check6').checked = false;
}
function cleanCheck4() {
    check = 4;
    document.getElementById('check2').checked = false;
    document.getElementById('check3').checked = false;
    document.getElementById('check1').checked = false;
    document.getElementById('check5').checked = false;
    document.getElementById('check6').checked = false;
}
function cleanCheck5() {
    check = 5;
    document.getElementById('check2').checked = false;
    document.getElementById('check3').checked = false;
    document.getElementById('check4').checked = false;
    document.getElementById('check1').checked = false;
    document.getElementById('check6').checked = false;
}
function cleanCheck6() {
    check = 6;
    document.getElementById('check2').checked = false;
    document.getElementById('check3').checked = false;
    document.getElementById('check4').checked = false;
    document.getElementById('check5').checked = false;
    document.getElementById('check1').checked = false;
}
function cleanCheckYes() {
    document.getElementById('CheckNo').checked = false;
    checkYesNo = 1;
}
function cleanCheckNo() {
    checkYesNo = 2;
    document.getElementById('CheckYes').checked = false;
}

//השיטה ממלא מחרוזת שבתוכה יש את כל הנתונים שצריך כדי להכניס למסד הנתונים
//שאלון, שאלה ותשובות חדשים
function buildAnsQuestStr(numAns) {
    var newQuest = "";
    //אם יש דרישת משתמש לפתיחת שאלון חדש
    if (QuestionnaireSelect == -2) {//opene new questionnaire
        //שמור הרשאת גישה
        var permit = $("#MainContent_select_permit").val();
        //newQuest= ditals of the new questionnaire
        //שם השאלון
        newQuest = document.getElementById('Newquestionn').value;
        newQuest += "#";
        //לתוך איזה קורס הוא יתווסף 
        newQuest += $("#MainContent_select_Course").val();
        newQuest += "#";
        //מוסיף  הרשאה
        newQuest += permit;
    }
    //בודק אם התשובות הם כן, לא או תשובה פתוחה
    //מלכתחילה בתוך משתנה זה כבר קיים התשובות מריבוי תשובות
    if (document.getElementById('CheckYes').checked == true) {
        StrAns = "YES";
    }
    else if (document.getElementById('CheckNo').checked == true) {

        StrAns = "NO";
    }
    else if(numAns==12){//מצב של שאלה פתוחה הפונק מקבלת מס 12
         StrAns = document.getElementById('MainContent_openAnswerID').value.toString();
    }
    //ditayls ans and quest
    //מאתחל את המחרוזת  עם התשובות, השאלון, השאלה ומספר התשובות הקיימות
    var AllAnsStr = StrAns
    AllAnsStr += "#";
    AllAnsStr += QuestionnaireSelect;
    AllAnsStr += "#";
    AllAnsStr += document.getElementById('MainContent_question1').value.toString();
    AllAnsStr += "#";
    AllAnsStr += numAns;
    //newQuest=null- not new questionnare
    if (newQuest != "") {//אם נוסף גם שאלון חדש 
        AllAnsStr += "@";
        AllAnsStr += newQuest;
    }
    return AllAnsStr;
}

function SaveAmericanAnsAndQuest(numCorectAns) {
    var AllAnsStr =numCorectAns+"#"+ buildAnsQuestStr(numAns);
    $.ajax({
        type: "POST",
        url: "AddQuestion.aspx/save_ClientClick",
        data: '{AllAnsStr: "' + AllAnsStr + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
        },
        failure: function (response) {
        }

    });
}
function SaveYesNoAnsAndQuest() {
    var AllAnsStr ="nullAns#"+ buildAnsQuestStr(11);

    $.ajax({
        type: "POST",
        url: "AddQuestion.aspx/save_ClientClick",
        data: '{AllAnsStr: "' + AllAnsStr + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
        },
        failure: function (response) {
        }

    });
}

function SaveOpenAnsAndQuest() {

    var AllAnsStr = "nullAns#"+ buildAnsQuestStr(12);
   
        $.ajax({
            type: "POST",
            url: "AddQuestion.aspx/save_ClientClick",
            data: '{AllAnsStr: "' + AllAnsStr + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
            },
            failure: function (response) {
            }

        });
    
  
}
//מרוקן את כל השדות בשמירת שאלה- מכין אותם לשאלה הבאה
function clean(){
    //document.getElementById("#MainContent_select_Course").val() = -1;
    //document.getElementById("#MainContent_selected_Questionnaires").val() = -1;
    document.getElementById('CheckYes').checked = false;
    document.getElementById('CheckNo').checked = false;

    document.getElementById('answer1').value = '';
    document.getElementById('answer2').value = '';
    document.getElementById('answer3').value = '';
    document.getElementById('answer4').value = '';
    document.getElementById('answer5').value = '';
    document.getElementById('answer6').value = '';

    document.getElementById('check1').checked = false;
    document.getElementById('check2').checked = false;
    document.getElementById('check3').checked = false;
    document.getElementById('check4').checked = false;
    document.getElementById('check5').checked = false;
    document.getElementById('check6').checked = false;

    document.getElementById('MainContent_openAnswerID').value = '';

    document.getElementById('MainContent_question1').value = '';

    document.getElementById('err').style.display = 'none';


}
// save quest click
//שיטה שבהתאם לסוג השאלה שולחת לבדיקת תקינות של התשובות
//ושולחת לשיטה המתאימה כדי למלא את המסד נתונים
$(document).ready(function () {
    $("#MainContent_SaveAnswer").click(function () {
        var numberAns = validSelect();
        var numCorectAns = findeCorectAns();
        if (numberAns == -1) {//err 
            return;
        }
        var indexOfAns = validAmericanQ();
        if (typeQuestion == 1) {//american
            if (indexOfAns == -1) {
                return;
            }
            else {
                if (IsAnsChoose(numCorectAns, indexOfAns) == -1) {
                    return;
                }
                SaveAmericanAnsAndQuest(numCorectAns);
            }
        }
        else if (typeQuestion == 2) {//yesNo quest
            if (isYesOrNoChoose() == -1) {
                return;
            }
            SaveYesNoAnsAndQuest();
        }
        else {//open quest
            if (isOpenQuestNull() == -1) {
                return;
            }
            SaveOpenAnsAndQuest();
        }
        clean();

    });
});
//בודק אם התשובה לשאלה הפתוחה ריקה
function isOpenQuestNull() {
    if (document.getElementById('MainContent_openAnswerID').value == "") {
        document.getElementById('err').style.display = 'inline';
        document.getElementById('err').value = "מלא תשובה נכונה*";
        return -1;
    }

}
//בודק אם לא נבחרה כלל תשובה בכן ולא
function isYesOrNoChoose() {
    if (document.getElementById('CheckYes').checked == false && document.getElementById('CheckNo').checked == false) {
        document.getElementById('err').style.display = 'inline';
        document.getElementById('err').value = "יש לבחור תשובה נכונה*";
        return -1;
    }
}
//בודק אם המשתמש בחר לאיזה קורס ושאלון להוסיף את השאלה
function validSelect() {
    if ($("#MainContent_select_Course").val() == -1 || $("#MainContent_selected_Questionnaires").val() == -1) {
        document.getElementById('err').style.display = 'inline';
        document.getElementById('err').value = "בחר קורס ושאלון*";
        return -1;
    }
}
// מגלה מה התשובה הנכונה שנבחרה בשאלת ריבוי תשובות
function findeCorectAns() {
    if(document.getElementById('check1').checked == true){
        return 1;
    }
    else if(document.getElementById('check2').checked == true){
        return 2;
    }
    else if (document.getElementById('check3').checked == true) {
        return 3;
    }
    else if (document.getElementById('check4').checked == true) {
        return 4;
    }
    else if (document.getElementById('check5').checked == true) {
        return 5;
    }
    else if (document.getElementById('check6').checked == true) {
        return 6;
    }
    else
    {
        return 0;
    }

}
//בודק כמה תשובות המשתמש הכניס ושנכנס לפחות תשובה אחת
function validAmericanQ() {
    var idCheckChecked=0;
    StrAns = '';
    if (document.getElementById('answer1').value.toString() =='') {
        document.getElementById('err').style.display = 'inline';
        document.getElementById('err').value = "מלא לפחות תשובה אחת*";
        return -1;
    }
    else {
        numAns=1;
        StrAns = document.getElementById('answer1').value.toString();
    }
    if (document.getElementById('answer2').value.toString() == '') {
      
            return numAns;
    }
    else {
        numAns=2;
        StrAns += "#" + document.getElementById('answer2').value.toString();
 
    }
    if (document.getElementById('answer3').value.toString() == '') {
            return numAns;
    }
    else {
        numAns=3;
        StrAns += "#" + document.getElementById('answer3').value.toString();
    }
    if (document.getElementById('answer4').value.toString() == '') {
        return numAns;
    }
    else {
        numAns=4;
        StrAns += "#" + document.getElementById('answer4').value.toString();
    }
    if (document.getElementById('answer5').value.toString() == '') {
        return numAns;
    }
    else {
        numAns=5;
        StrAns += "#" + document.getElementById('answer5').value.toString();
    }
    if (document.getElementById('answer6').value.toString() == '') {
        return numAns;
    }
    else {
        numAns=6;
        StrAns = +"#" + document.getElementById('answer6').value.toString();
    }
   
}
//בודק האם נבחרה תשובה אחת נכונה לפחות ושהיא אכן חלק מהתשובות שהמשתמש הכניס
function IsAnsChoose(idCheckChecked, numAns) {
    alert(idCheckChecked + "  and  " + numAns);
    if (idCheckChecked > numAns || idCheckChecked == 0) {
        document.getElementById('err').style.display = 'inline';
        document.getElementById('err').value = "יש לבחור תשובה נכונה*";
        return -1;
    }
}