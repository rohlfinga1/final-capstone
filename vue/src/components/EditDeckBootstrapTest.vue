<template>
  <div id="app">
    <b-table :items="cards" :fields="fields">
      <template slot="front" slot-scope="row">
          <b-form-input v-if="cards[row.item.index].isEdit" type="text" v-model="cards[row.item.index].front"></b-form-input>
          <span v-else>{{row.item.front}}</span>
      </template>
      <template #cell(back)="data">
        <b-form-select v-if="cards[data.index].isEdit" v-model="cards[data.index].back"></b-form-select>
        <span v-else>{{data.value}}</span>
      </template>
      <template #cell(keywords)="data">
          <b-form-input v-if="cards[data.index].isEdit" type="number" v-model="cards[data.index].keywords"></b-form-input>
          <span v-else>{{data.value}}</span>
      </template>
      <template #cell(edit)="data">
        <b-button @click="editRowHandler(data)">
          <span v-if="!cards[data.index].isEdit">Edit</span>
          <span v-else>Submit</span>
        </b-button>
      </template>
    </b-table>
     <pre>
      {{items}}
    </pre>
  </div>
</template>
<script>
import { mapState } from 'vuex';

export default {
  name: "edit-deck",
  components: {},
  data() {
    return {
        cards: [],
        fields: [
            { key: "front", label: "Front"},
            { key: "back", label: "Back" },
            { key: "keywords", label: "Keywords" },
            { key: 'edit', label: ''}
        ],
       
    };
  },
  computed: mapState(['cards']),

  mounted() {
      this.items = this.items.map(item => ({...item, isEdit: false}));
  },
  methods: {
      editRowHandler(data) {
        this.items[data.index].isEdit = !this.items[data.index].isEdit;
      }
    }
  }
</script>
<style>
#app {
  text-align: center;
  margin: 60px;
}
thead, tbody, tfoot, tr, td, th {
  text-align: left;
  width: 100px;
  vertical-align: middle;
}
pre {
  text-align: left;
  color: #d63384;
}
</style>