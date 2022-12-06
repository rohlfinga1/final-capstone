import axios from 'axios';

export default 
{
GetDecks(){
    return axios.get('/myDecks');
},

AddDeck(newDeck){
    return axios.post('/myDecks', newDeck)

    
}


}