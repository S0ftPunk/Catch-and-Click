mergeInto(LibraryManager.library, {
  InitPurchases: function() {
    initPayments();
  },

  Purchase: function(id) {
    buy(id);
  },
  SetLeaderBoard: function(scores){
    SaveToLeaderBoard(scores);
  },
  AuthenticateUser: function() {
    auth();
  },

  GetUserData: function() {
    getUserData();
  },

  ShowFullscreenAd: function () {
    showFullscrenAd();
  },

  ShowRewardedAd: function(placement) {
    showRewardedAd(placement);
    return placement;
  },

  GetLang: function() {
      var lang = GetLanguage();
      var bufferSize = lengthBytesUTF8(lang) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(lang, buffer,bufferSize);
      console.log(buffer);
    return buffer;
  },
  
  OpenWindow: function(link){
    var url = Pointer_stringify(link);
      document.onmouseup = function()
      {
        window.open(url);
        document.onmouseup = null;
      }
  }
});