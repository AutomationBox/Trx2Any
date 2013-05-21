using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trx2Any.Common
{
    /// <summary>
    /// DI module for the Framework. This is used to register the Type-object collections to be used across application.
    /// </summary>
    /// <remarks>This is an IOC container which registers object so that they can be used anywhere in the application with a key. 
    /// In this framework, it is the type of the object you need.</remarks>
    public static class IocService
    {
        //Dictionary to hold the Registered types
        static readonly Dictionary<Type, object> RegisteredTypes = new Dictionary<Type, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toRegister">object to register</param>
        /// <exception cref="SeleniumUITestFrameworkException"></exception>
        /// <typeparam name="T">T is the typeof(toRegister)</typeparam>
        /// <remarks></remarks>
        public static void Register<T>(T toRegister)
        {



            if (RegisteredTypes.Keys.Contains(typeof(T)))
            {
                /*IocService.Resolve<TestLogger>().Write(
                    string.Format(FrameworkMessageResourceFile.IocService_Register_ObjectTypeAlreadyExists,
                                  typeof(T).ToString()), LogStatus.Message);
                throw new SeleniumUITestFrameworkException(MethodBase.GetCurrentMethod().DeclaringType.FullName,
                                                           System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                           string.Format(
                                                               FrameworkMessageResourceFile.
                                                                   IocService_Register_ObjectTypeAlreadyExists,
                                                               typeof(T).FullName));
                 */

            }
            RegisteredTypes.Add(typeof(T), toRegister);

        }

        /// <summary>
        /// Resolves the registered instance of Type T
        /// </summary>
        /// <returns>Object with the specified type registered in container</returns>
        /// <typeparam name="T">object type to register. Or ClassName/Interface Name for which object has already been registered</typeparam>
        /// <remarks></remarks>
        public static T Resolve<T>()
        {
            //if (RegisteredTypes.Keys.Contains(typeof(T)) == false)
            //{
            //    /*IocService.Resolve<TestLogger>().Write(string.Format(FrameworkMessageResourceFile.IocService_Resolve_ObjectTypeAlreadyExists, typeof(T).ToString()), LogStatus.Message);

            //    throw new SeleniumUITestFrameworkException(MethodBase.GetCurrentMethod().DeclaringType.FullName,
            //                                               System.Reflection.MethodBase.GetCurrentMethod().Name,
            //                                               string.Format(
            //                                                   FrameworkMessageResourceFile.
            //                                                       IocService_Register_ObjectTypeAlreadyExists,
            //                                                   typeof(T).FullName));
            //     * */
            //}
            //else
                return (T)RegisteredTypes[typeof(T)];

        }

        /// <summary>
        /// Replaces the old registered object with the provided one.
        /// </summary>
        /// <param name="toRegister">object to register</param>
        /// <typeparam name="T">T is the typeof(toRegister)</typeparam><remarks></remarks>
        public static void Refresh<T>(T toRegister)
        {
            if (RegisteredTypes.Keys.Contains(typeof(T)))
            {
                RegisteredTypes[typeof(T)] = toRegister;
            }
            else
            {
                //IocService.Resolve<TestLogger>().Write(string.Format(FrameworkMessageResourceFile.IocService_Resolve_ObjectTypeAlreadyExists, typeof(T).ToString()), LogStatus.Message);
            }
        }

        /// <summary>
        /// Clears all the instances from the dictionary/IOC Container
        /// </summary>
        /// <param name="toRegister">object to register</param>
        /// 
        /// <typeparam name="T">T is the typeof(toRegister)</typeparam><remarks></remarks>
        public static void RegisterOrRefresh<T>(T toRegister)
        {
            if (RegisteredTypes.Keys.Contains(typeof(T)))
            {
                RegisteredTypes[typeof(T)] = toRegister;
                //IocService.Resolve<TestLogger>().Write(string.Format(FrameworkMessageResourceFile.IocService_RegisterOrRefresh_ObjectRefresh, typeof(T).ToString()), LogStatus.Message);
            }
            else
            {
                RegisteredTypes.Add(typeof(T), toRegister);
                //IocService.Resolve<TestLogger>().Write(string.Format(FrameworkMessageResourceFile.IocService_RegisterOrRefresh_ObjectRegistered, typeof(T).ToString()), LogStatus.Message);
            }
        }

        /// <summary>
        /// Clears all the instances from the dictionary/IOC Container
        /// </summary>
        /// <remarks></remarks>
        public static void ReleaseAll()
        {
            try
            {
                RegisteredTypes.Clear();
            }
            catch (Exception ex)
            {
                /*throw new SeleniumUITestFrameworkException(MethodBase.GetCurrentMethod().DeclaringType.FullName,
                                                           System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                           "Release all failed", ex);
                */
            }

        }
    }
}
