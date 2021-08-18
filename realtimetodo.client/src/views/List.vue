<template>
  <div>
    <h1>List: {{ list.name }}</h1>
    <hr/>
    <table>
      <thead>
      <tr>
        <th>&nbsp;</th>
        <th>Task</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="i in list.items" :key="i.id">
        <td>
          <input type="checkbox" v-model="i.isCompleted"/>
        </td>
        <td>{{ i.text }}</td>
      </tr>
      <tr>
        <th>&nbsp;</th>
        <th>
          <input type="text" v-model.trim="newItemText">
          <button @click="addNewItem">+</button>
        </th>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'

@Component
export default class List extends Vue {
  listId = -1
  newItemText = ''

  list: any = {
    name: '',
    items: []
  }

  addNewItem () {
    if (this.newItemText === '') return

    Vue.$connectionService.addToDoItem(this.listId, this.newItemText)
    this.newItemText = ''
  }

  created () {
    this.listId = this.$route.params.listId as unknown as number
    console.log(`List id:${this.listId} created`)

    Vue.$connectionService.events.on('updatedListData', (data: any) => {
      this.list = data
    })
    Vue.$connectionService.getListData(this.listId)
    Vue.$connectionService.subscribeToListUpdates(this.listId)
  }

  destroyed () {
    console.log(`List id:${this.listId} destroyed`)
    Vue.$connectionService.unsubscribeToListUpdates(this.listId)
  }
}
</script>

<style>

</style>
