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
        <td>
          <router-link :to="{name: 'list', params: {listId: l.id}}">{{ l.name }}</router-link>
        </td>
        <td>{{ l.items | pendingCount }}</td>
        <td>{{ l.items | completedCount }}</td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'

@Component({
  filters: {
    pendingCount: (value: any) => {
      const r = value.filter((p: any) => !p.isCompleted)
      return r.length
    },
    completedCount: (value: any) => {
      const r = value.filter((p: any) => p.isCompleted)
      return r.length
    }
  }
})
export default class Home extends Vue {
  lists: any[] = []

  async created () {
    Vue.$connectionService.events.on('updatedToDoList', (values: any[]) => {
      this.lists = values
    })
    await this.$connectionService.getLists()
    console.log('Home get lists:', this.lists)
  }
}
</script>
