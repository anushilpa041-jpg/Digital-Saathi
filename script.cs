// Section switching
function showSection(id, btn){
  document.querySelectorAll('.section').forEach(s=>s.classList.remove('active'));
  document.querySelectorAll('.nav button').forEach(b=>b.classList.remove('active'));
  document.getElementById(id).classList.add('active');
  btn.classList.add('active');
  window.scrollTo(0,0);
}

// SOS
function sosAlert(){
  document.getElementById('sos-msg').innerHTML = "📢 SOS Sent! Calling 108... Location Shared.";
  if(navigator.vibrate) navigator.vibrate([500,200,500]);
  setTimeout(()=>{ window.location.href="tel:108"; }, 1000);
}

// Medicine Reminder - LocalStorage
let meds = JSON.parse(localStorage.getItem('meds') || '[]');

function addMedicine(){
  let name = document.getElementById('medName').value.trim();
  let time = document.getElementById('medTime').value;
  if(!name || !time) return alert("Enter medicine name & time");
  meds.push({name, time});
  localStorage.setItem('meds', JSON.stringify(meds));
  document.getElementById('medName').value="";
  renderMeds();
}

function renderMeds(){
  let list = document.getElementById('medList');
  if(!list) return;
  list.innerHTML = meds.map((m,i)=>
    `<div class="tip">💊 <b>${m.name}</b> - ${m.time} <span onclick="deleteMed(${i})" style="float:right;cursor:pointer;color:red"> ❌ </span></div>`
  ).join('');
}

function deleteMed(i){
  meds.splice(i,1);
  localStorage.setItem('meds', JSON.stringify(meds));
  renderMeds();
}

renderMeds();

// Auto alarm check every 60 sec
setInterval(()=>{
  let now = new Date().toTimeString().slice(0,5);
  meds.forEach(m=>{
    if(m.time === now){
      alert("💊 Time to take medicine: " + m.name);
      try{ new Audio('https://actions.google.com/sounds/v1/alarms/beep_short.ogg').play(); }catch(e){}
    }
  });
}, 60000);

// Contacts
function addContact(){
  let n = document.getElementById('cName').value.trim();
  let num = document.getElementById('cNum').value.trim();
  if(!n || !num) return alert("Enter name & number");
  document.getElementById('contactList').innerHTML += `<p><strong>${n}:</strong> ${num}</p>`;
  document.getElementById('cName').value="";
  document.getElementById('cNum').value="";
}