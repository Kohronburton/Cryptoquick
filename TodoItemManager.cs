using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace CryptoQuick
{
	public partial class TodoItemManager
	{
		static TodoItemManager defaultInstance = new TodoItemManager ();

		const string accountURL = @"https://cryptoquickcosmos.documents.azure.com:443/";
		const string accountKey = @"FtCGjYLClodOLDCTcAdDjVw2bejjG6jq5r9VRZ2Hu0RcwNF3HEynoQdfsML5tv03TP2Y4aDt28T33aaVZBG9Og==";
		const string databaseId = @"ToDoList";
		const string collectionId = @"Items";

		private Uri collectionLink = UriFactory.CreateDocumentCollectionUri (databaseId, collectionId);

		private DocumentClient client;

		private TodoItemManager ()
		{
			client = new DocumentClient (new System.Uri (accountURL), accountKey);
		}

		public static TodoItemManager DefaultManager {
			get {
				return defaultInstance;
			}
			private set {
				defaultInstance = value;
			}
		}

		public List<TodoItem> Items { get; private set; }

		public async Task<List<TodoItem>> GetTodoItemsAsync ()
		{
			try {
				// The query excludes completed TodoItems
				var query = client.CreateDocumentQuery<TodoItem> (collectionLink, new FeedOptions { MaxItemCount = -1 })
					  .Where (todoItem => todoItem.Complete == false)
					  .AsDocumentQuery ();

				Items = new List<TodoItem> ();
				while (query.HasMoreResults) {
					Items.AddRange (await query.ExecuteNextAsync<TodoItem> ());
				}


			} catch (Exception e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
				return null;
			}

			return Items;
		}

		public async Task<TodoItem> InsertItemAsync (TodoItem todoItem)
		{
			try {
				var result = await client.CreateDocumentAsync (collectionLink, todoItem);
				todoItem.Id = result.Resource.Id;
				Items.Add (todoItem);


			} catch (Exception e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
			return todoItem;
		}

		public async Task CompleteItemAsync (TodoItem item)
		{
			try {
				item.Complete = true;
				await client.ReplaceDocumentAsync (UriFactory.CreateDocumentUri (databaseId, collectionId, item.Id), item);

				Items.Remove (item);

			} catch (Exception e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
		}
	}
}
