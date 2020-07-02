function isolateFirstLastName(user) {

    let split = user.split('');
    
    let newText = [];
    let switchMaj = false;

    for (let i = 0; i < split.length; i++) {
        if (split[i] !== '.' && split[i] !== '@') {
            if(i === 0 || switchMaj) {
                newText.push(split[i].toUpperCase());
            } else {
                newText.push(split[i]);
            }
        } else if (split[i] === '.'){
            newText.push(' ');
            switchMaj = true;
        } else {
            return newText.join('');
        }
    }
}