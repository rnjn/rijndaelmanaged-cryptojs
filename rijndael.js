var rijndaelEncrypt = function(){
  document.getElementById('encrypted').innerText = "";
  var plainText = document.getElementById('plain').value;
  var key = document.getElementById('key').value;
  var encryptedText = encrypt(plainText, key);
  document.getElementById('encrypted').innerText = encryptedText;
}

var encrypt = function(plainText, key){
  var C = CryptoJS;
  plainText = C.enc.Utf8.parse(plainText);
  console.log(plainText);
  key = C.enc.Utf8.parse(key);
  var aes = C.algo.AES.createEncryptor(key, {
    mode: C.mode.CBC,
    padding: C.pad.Pkcs7,
    iv: key
  });
  var encrypted = aes.finalize(plainText);
  return C.enc.Base64.stringify(encrypted);
}
