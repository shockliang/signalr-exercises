<template>
  <div class="home">
    <h1>To Do List</h1>
    <br/>
    <table>
      <thead>
      <tr>
        <th>List Name</th>
        <th>Pending</th>
        <th>Completed</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="l in lists" :key="l.id">
        <td>{{ l.listName }}</td>
        <td>{{ l.pending }}</td>
        <td>{{ l.completed }}</td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'

@Component
export default class Home extends Vue {
  lists: any[] = [];

  demoList () {
    this.lists.push({ id: 0, listName: 'Foo', completed: 5, pending: 10 })
    this.lists.push({ id: 1, listName: 'Bar', completed: 4, pending: 14 })
    this.lists.push({ id: 2, listName: 'Walking', completed: 1, pending: 20 })
    this.lists.push({ id: 3, listName: 'Others', completed: 0, pending: 30 })
  }

  created () {
    this.demoList()
  }
}
</script>
