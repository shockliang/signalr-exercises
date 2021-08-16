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

    this.connection.on('updatedListData', (value: any) => {
      this.events.emit('updatedListData', value)
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

  async getListData (id: number) {
    if (this.connection.state === HubConnectionState.Connected) {
      await this.connection.send('GetList', id)
    } else {
      setTimeout(async () => await this.getListData(id), 500)
    }
  }

  async subscribeToCountUpdates () {
    if (this.connection.state === HubConnectionState.Connected) {
      await this.connection.send('SubscribeToCountUpdates')
    } else {
      setTimeout(async () => await this.subscribeToCountUpdates(), 500)
    }
  }

  async unsubscribeToCountUpdates () {
    if (this.connection.state === HubConnectionState.Connected) {
      await this.connection.send('UnsubscribeToCountUpdates')
    } else {
      setTimeout(async () => await this.unsubscribeToCountUpdates(), 500)
    }
  }

  async subscribeToListUpdates (id: number) {
    if (this.connection.state === HubConnectionState.Connected) {
      await this.connection.send('SubscribeToListUpdates', id)
    } else {
      setTimeout(async () => await this.subscribeToListUpdates(id), 500)
    }
  }

  async unsubscribeToListUpdates (id: number) {
    if (this.connection.state === HubConnectionState.Connected) {
      await this.connection.send('UnsubscribeToListUpdates', id)
    } else {
      setTimeout(async () => await this.unsubscribeToListUpdates(id), 500)
    }
  }

  async addToDoItem (listId: number, text: string) {
    if (this.connection.state === HubConnectionState.Connected) {
      await this.connection.send('AddToDoItem', listId, text)
    } else {
      setTimeout(async () => await this.addToDoItem(listId, text), 500)
    }
  }

  async toggleToDoItem (listId: number, itemId: number) {
    if (this.connection.state === HubConnectionState.Connected) {
      await this.connection.send('ToggleToDoItem', listId, itemId)
    } else {
      setTimeout(async () => await this.toggleToDoItem(listId, itemId), 500)
    }
  }
}

export const ConnectionServices: PluginObject<any> = {
  install (Vue: VueConstructor<Vue>, option: any | undefined) {
    Vue.$connectionService = new ToDoService()
    Vue.prototype.$connectionService = Vue.$connectionService
  }
}
