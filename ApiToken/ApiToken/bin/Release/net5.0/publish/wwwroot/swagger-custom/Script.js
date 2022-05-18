(function () {
  window.addEventListener("load", function () {
    setTimeout(function () {
      var logo = document.getElementsByClassName('link');
      logo[0].href = "https://api.tarikdavulcu.com/";
      logo[0].target = "_blank";

      logo[0].children[0].alt = "Tarık Davulcu";
      logo[0].children[0].src = "swagger-custom/logo.png";

      var elements = document.getElementsByClassName('title');
      if (elements.length > 0) {
        var content = elements[0].textContent;
        document.title = content;
        updateTitle();
      }
      else {
        setTimeout(updateTitle, 1000);
      }
      'My Api v2 '

      
    });
  });
})();
function updateTitle() {

  var link = document.querySelector("link[rel*='icon']") || document.createElement('link');;
  document.head.removeChild(link);
  link = document.querySelector("link[rel*='icon']") || document.createElement('link');
  document.head.removeChild(link);
  link = document.createElement('link');
  link.type = 'image/x-icon';
  link.rel = 'shortcut icon';
  link.href = 'swagger-custom/logo.png';
  document.getElementsByTagName('head')[0].appendChild(link);

  var elements = document.getElementsByClassName('title');
  if (elements.length > 0) {
    var content = elements[0].textContent;
    document.title = content;
  }
 
}



var elements = document.getElementsByClassName('title');
if (elements.length > 0) {
  var content = elements[0].textContent;
  document.title = content;
}

