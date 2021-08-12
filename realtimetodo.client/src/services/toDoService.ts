import Vue, { PluginObject, VueConstructor } from 'vue'
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from '@microsoft/signalr'
import { EventEmitter } from 'events'

export default class ToDoService {
  connection: HubConnection
  events: EventEmitter

  constructor () {
    this.events = new EventEmitter()
    this.connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Trace)
      .withAutomaticReconnect()
      .withUrl('/hubs/todo')
      .build()

    this.connection.on('updatedToDoList', (values: any[]) => {
      this.events.emit('updatedToDoList', values)
    })
  }

  async start () {
    await this.connection.start()
  }

  async getLists () {
    if (this.connection.state === HubConnectionState.Connected) {
      await this.connection.send('GetLists')
    } else {
      setTimeout(async () => await this.getLists(), 500)
    }
  }
}

export const ConnectionServices: PluginObject<any> = {
  install (Vue: VueConstructor<Vue>, option: any | undefined) {
    Vue.$connectionService = new ToDoService()
    Vue.prototype.$connectionService = Vue.$connectionService
  }
}
