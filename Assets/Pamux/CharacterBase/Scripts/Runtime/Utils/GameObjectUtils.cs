using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pamux.Lib.Unity.Commons
{
    public static class GameObjectUtils
    {
        public static void CopyTo(this Component from, Component to) {
            Type type = from.GetType();
            if (type != to.GetType()) {
                return; // TODO: throw
            }

            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

            foreach (var pinfo in type.GetProperties(flags)) {
                if (pinfo.CanWrite) {
                    try {
                        pinfo.SetValue(to, pinfo.GetValue(from, null), null);
                    }
                    catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
                }
            }

            foreach (var finfo in type.GetFields(flags)) {
                finfo.SetValue(to, finfo.GetValue(from));
            }
        }
    }
}