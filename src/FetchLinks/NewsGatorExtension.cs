using System;
using System.Xml;

namespace NGExtension
{
	public interface INewsGatorExtension
	{
		/// <summary>
		/// Called once, on one instance of your extension, when NewsGator is
		/// about to begin processing new items.
		/// </summary>
		void BeginRetrieve();

		/// <summary>
		/// Called while an item is being processed. If this function is called,
		/// NewsGator is processing an item that appears to be "new", and the 
		/// process of adding it to Outlook has been started.
		/// 
		/// This function will be called on one of the NewsGator retrieval threads.
		/// There are 1-5 of these threads, controlled by the "performance" setting
		/// in NewsGator/Options, Options tab.  You MUST NOT access the Outlook
		/// object model during this function.
		/// 
		/// Instance management is subject to change.  This means that NewsGator
		/// may call PreProcessItem 3 times, then PostProcessItem 3 times, or it
		/// may interleave the calls.  Use the reference object returned by 
		/// PreProcessItem to correlate Pre- and Post- calls. 
		/// </summary>
		/// <param name="postInfo">
		/// This is the normalized information NewsGator has parsed from the RSS item.
		/// This information will be sent to the code that actually creates the 
		/// PostItem in Outlook; any changes you make to this object will be 
		/// carried forward and will affect the Outlook PostItem.
		/// </param>
		/// <param name="originalItem">
		/// This is the actual RSS item XML for the item being processed.  If your
		/// extension will process custom extensions, for example, you will find
		/// them here.  The node element will be named originalItem, and the actual
		/// RSS item will be the first child of this element.
		/// </param>
		/// <param name="createPost">
		/// Output parameter, defines whether or not the post should be created in
		/// Outlook.  If you return with this parameter set to true, PostProcessItem
		/// will still be called, but its postItem parameter will be null.
		/// </param>
		/// <returns>
		/// Returns a reference object which will be passed to PostProcessItem as the
		/// reference parameter.
		/// </returns>
		object PreProcessItem(PostInfo postInfo, XmlNode originalItem, out bool createPost);

		/// <summary>
		/// Called when the PostItem in Outlook has been created (assuming 
		/// createPost from PreProcessItem was true).  
		/// 
		/// This function will be called from the Outlook GUI thread.  This means
		/// you may access the Outlook object model through the passed in objects;
		/// however, keep the amount of processing done in this function to an
		/// absolute minimum, to preserve the user experience.
		/// </summary>
		/// <param name="reference">
		/// Reference object that the extension returned from PreProcessItem.
		/// </param>
		/// <param name="postItem">
		/// This will be the just-created PostItem object in Outlook.  If you wish
		/// to manipulate this object, add a reference to the Outlook XP Primary
		/// Interop Assembly, and cast the object to a PostItem.
		/// 
		/// If PreProcessItem returned createPost=false, then the postItem 
		/// parameter will be null.
		/// </param>
		/// <param name="appObj">
		/// The Outlook application object, which you can use to manipulate other
		/// parts of the Outlook object model.  If you wish
		/// to manipulate this object, add a reference to the Outlook XP Primary
		/// Interop Assembly, and cast the object to a Application.
		/// </param>
		void PostProcessItem(object reference, object postItem, object appObj);

		/// <summary>
		/// Called once, on the same instance of your extension that BeginRetrieve
		/// was called on, when NewsGator has completed retrieving new items.
		/// </summary>
		void EndRetrieve();
	}

	public class PostInfo
	{
		public string Title;
		public string Description;
		public string FeedName;
		public DateTime pubDate;
		public string Author;
		public string Categories;
		public string FromAddr;
		public string PostLink;
	}
}
