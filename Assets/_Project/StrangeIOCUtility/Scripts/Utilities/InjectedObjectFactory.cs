using strange.extensions.injector.api;
using strange.extensions.injector.impl;
using UnityEngine;

namespace _Project.StrangeIOCUtility.Scripts.Utilities
{
    //THIS UTILITY CLASS IS RESPONSIBLE TO PROVIDE A WAY TO INJECT ANYTHING (STRANGE IOC ALLOWS) INTO PLAIN OLD C# CLASSES.
    //IN STRANGE IOC, MODELS AND SERVICES ARE POCO CLASSES THAT CAN USE [INJECT] ANNOTATION.
    //MOST OF THE TIME WE ONLY NEED TO INJECT INTO VIEW, MEDIATOR, AND COMMAND.
    //IF THERE IS A GOOD REASON TO INJECT ANYTHING ELSE:
    //----1. CONSULT THE TEAM AND MAKE SURE THIS IS A VALID USE CASE.
    //----2. INSIDE THE CLASS TO BE INJECTED, MAKE SURE YOU HAVE AN EMPTY CONSTRUCTOR
    //----3. INSIDE THE CLASS TO BE INJECTED, HAVE PUBLIC PROPERTIES WITH [INJECT] ANNOTATION LIKE:
    //-------[Inject] public IPlayerProfileModel PlayerProfileServiceModel { get; set; }
    //----4. USE GetInjectedInstance METHOD TO NOTIFY STRANGE IOC SYSTEM ABOUT THE CLASS AND PERFORM SETTER INJECTION
    //----5. SINCE WE HAVE PARAMETERLESS CONSTRUCTOR, ONLY SETTER INJECTED PROPERTIES ARE FILLED.
    //----6. TO FILL OTHER PROPERTIES, YOU CAN WRITE ANOTHER CUSTOM METHOD SUCH AS FillParameters OR FillParameter.
    
    //Note: Since only the InjectionBinder of Main Context is referenced here,
    //Keep in mind that you need to Bind an injection before you want to inject it into a class.
    //Best and guaranteed way to do this is to bind it in MainContext.

    public class InjectedObjectFactory
    {
        public static IInjectionBinder InjectionBinder { get; set; }
        public InjectedObjectFactory(InjectionBinder injectionBinder)
        {
            InjectionBinder = injectionBinder;
        }

        public static T GetInjectedInstance<T>() where T : new()
        {
            T objectInstance = new T();

            InjectionBinder.injector.Inject(objectInstance);
            
            return objectInstance;
        }        
        
        public static T GetInjectedInstance<T>(T monoBehaviour) where T : MonoBehaviour//TODO: Test
        {
            InjectionBinder.injector.Inject(monoBehaviour);
            
            return monoBehaviour;
        }
    }
}