//Disable back button

//function disableBackButton()
//{
//    window.history.forward(1);
//}

//Logout Message
function LogoutMessage()
{
    var ht = document.getElementsByTagName("html");
    ht[0].style.filter="progid:DXImageTransform.Microsoft.BasicImage(grayscale=1)";
    if(confirm('Are you sure you want to log out of IAP application?'))
    {
        return true;
    }
    else
    {
        ht[0].style.filter="";
        return false;
    }
}

//Delete Request
function DeleteIP(proposal)
{
    //    var ht = document.getElementsByTagName("html");
    //    ht[0].style.filter="progid:DXImageTransform.Microsoft.BasicImage(grayscale=1)";
    if(confirm('Are you sure you want to delete '+ proposal + ' proposal ?'))
    {
        return true;
    }
    else
    {
        return false;
    }
}

function isNumberKey(evt)
{
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 46 || charCode > 57))
    {
        return false;
    }
    else if(charCode == 47)
    {
       return false;
    }
    else
    {
        return true;
    }
}

function isKeyOnPress(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode

    return false;
}

function OpenPopup(sUrl)
{
    window.open(sUrl, "requestForm", "scrollbars=yes, resizable=no, width=1000, height=500");
    return false;
}