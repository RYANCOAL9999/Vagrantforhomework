using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine.Purchasing;

namespace Koocell.UnityPurchase
{
	public class UnityPurchasingManager : IStoreListener {

#region Properties

		private bool _debugLog;
		private bool _isInited{
			get{
				return (_controller != null && _extensions != null);
			}
		}

		private ConfigurationBuilder _builder;
		private IStoreController _controller;
		private IExtensionProvider _extensions;

		public IStoreController GetStoreController() {
			return _controller;
		}
		public IExtensionProvider GetExtensionProvider() {
			return _extensions;
		}

		private IStoreListener _extendListener;

#endregion

#region Setup

		public UnityPurchasingManager() {
			_debugLog = false;

			_builder = null;
			_controller = null;
			_extensions = null;

			_extendListener = null;
		}

		public void Init(List<ProductDefinition> products){
			Init(products, null);
		}
		public void Init(List<ProductDefinition> products, IStoreListener listener)
		{
			if(_isInited){
				return ;
			}

			// Setup Config builder
			_builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

			// Setup store products
			if(products != null && products.Count > 0){
				HashSet<ProductDefinition> productSet = new HashSet<ProductDefinition>();
				foreach(ProductDefinition productDef in products){
					productSet.Add(productDef);
				}
				ReadOnlyCollection<ProductDefinition> productCollection = new ReadOnlyCollection<ProductDefinition>(productSet.ToList());
				_builder.AddProducts(productCollection);
			}

			// Setup
			_extendListener = listener;

			// Initialize
			UnityPurchasing.Initialize(this, _builder);
		}

#endregion

#region Controller

		public Product[] GetAllProducts(){
			if(!_isInited){
				return null;
			}
			return _controller.products.all;
		}

		public Product GetProductByID(string productId){
			if(!_isInited){
				return null;
			}
			return _controller.products.WithID(productId);
		}

		public Product GetProductByStoreSpecificID(string productId){
			if(!_isInited){
				return null;
			}
			return _controller.products.WithStoreSpecificID(productId);
		}

		public void PurchaseProduct(string productId){
			if(!_isInited){
				DebugError(string.Format("Fail to PurchaseProduct:{0}. UnityPurchasingManager has not setup", productId));
				return;
			}
			Product product = _controller.products.WithID(productId);
			if(product == null){
				DebugError(string.Format("Fail to PurchaseProduct:{0}. Can not find the product by id", productId));
				return;
			}
			PurchaseProduct(product);
		}
		public void PurchaseProduct(Product product){
			if(product == null){
				return;
			}
			if(!_isInited){
				DebugError(string.Format("Fail to PurchaseProduct:{0}. UnityPurchasingManager has not setup", product.definition.id));
				return;
			}

			_controller.InitiatePurchase(product);
		}

		public void ConfirmPendingPurchase(Product product){
			if(product == null){
				return;
			}
			if(!_isInited){
				DebugError(string.Format("Fail to ConfirmPendingPurchase with Product:{0}. UnityPurchasingManager has not setup", product.definition.id));
				return;
			}

			_controller.ConfirmPendingPurchase(product);
		}

#endregion

#region IStoreListener

		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			_controller = controller;
			_extensions = extensions;
			DebugLog("OnInitialized: PASS");
			if(_extendListener != null){
				_extendListener.OnInitialized(_controller, _extensions);
			}
		}

		public void OnInitializeFailed(InitializationFailureReason error)
		{
			DebugLog("OnInitialized FAIL. error: " + error.ToString());
			if(_extendListener != null){
				_extendListener.OnInitializeFailed(error);
			}
		}

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
		{
			DebugLog("ProcessPurchase Product: " + args.purchasedProduct.definition.id);
			if(_extendListener != null){
				return _extendListener.ProcessPurchase(args);
			}else{
				// Return a flag indicating whether this product has completely been received, or if the application needs 
				// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
				// saving purchased products to the cloud, and when that save is delayed. 
				return PurchaseProcessingResult.Complete;
			}
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
		{
			DebugLog(string.Format("OnPurchase: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, reason));
			if(_extendListener != null){
				_extendListener.OnPurchaseFailed(product, reason);
			}
		}

#endregion

#region Debug Log

		public void SetDebugLog (bool debugLog) {
			_debugLog = debugLog;
		}

		private void DebugLog (string logMsg) {
			if(_debugLog){
				Debug.Log(logMsg);
			}
		}

		private void DebugError (string errorMsg) {
			if(_debugLog){
				Debug.LogWarning(errorMsg);
			}else{
				Debug.LogError(errorMsg);
			}
		}

#endregion

	}
}