﻿using Autodesk.Revit.DB;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addin1Python
{
    public class CircleEquation
    {
        // Chiều dương là chiều ngược kim đồng hồ
        private double perimeter = -1;
        private XYZ center;
        

        public UV CenterUV { get; set; }
        public double Z { get; set; }
        public double Radius { get; set; }
        public List<List<Arc>> StandardArcs { get; set; } = new List<List<Arc>>();
        public int Number { get; set; }
        public double Angle { get; set; }
        
        public double Perimeter
        {
            get
            {
                if (perimeter == -1)
                    perimeter = Radius * 2 * Math.PI;
                return perimeter;
            }
        }
        public XYZ Center
        {
            get
            {
                if (center == null)
                    center = new XYZ(CenterUV.U, CenterUV.V, Z);
                return center;
            }
        }


        public CircleEquation(UV center, double radius)
        {
            CenterUV = center; Radius = radius; Z = 0;
        }
        public CircleEquation(XYZ center, double radius) : this(center, radius, center.Z) { }
        public CircleEquation(XYZ center, double radius, double z) : this(new UV(center.X, center.Y), radius) { Z = z; }
        public CircleEquation(Arc arc) : this(arc.Center, arc.Radius) { }

        public double ConvertLength2Angle(double length)
        {
            return length / Perimeter * (Math.PI * 2);
        }
        public XYZ ConvertUV2XYZ(UV uv)
        {
            return new XYZ(uv.U, uv.V, Z);
        }
        public XYZ GetEndPoint(double angle)
        {
            return ConvertUV2XYZ(GetEndPointUV(angle));
        }
        public UV GetEndPointUV(double angle)
        {
            return new UV(CenterUV.U + Radius * Math.Cos(angle),CenterUV.V +Radius * Math.Sin(angle));
        }
        public ArcInfo GetArcInfo(double startAngle, double endAngle)
        {
            return new ArcInfo(this, startAngle, endAngle);
        }
        public ArcInfo GetArcInfo2(double startAngle, double plusAngle)
        {
            return GetArcInfo(startAngle, startAngle+ plusAngle);
        }
        public XYZ GetEndPoint(double startAngle, double plusAngle)
        {
            return ConvertUV2XYZ(GetEndPointUV(startAngle, plusAngle));
        }
        public UV GetEndPointUV(double startAngle, double plusAngle)
        {
            return GetEndPointUV(startAngle + plusAngle);
        }
        public ArcInfo GetArcInfo(double startAngle, double length, bool isOtherwiseClock)
        {
            double plusAngle = isOtherwiseClock ? ConvertLength2Angle(length) : -ConvertLength2Angle(length);
            return GetArcInfo2(startAngle, plusAngle);
        }
        public XYZ GetEndPoint(double startAngle, double length, bool isOtherwiseClock)
        {
            return ConvertUV2XYZ(GetEndPointUV(startAngle, length, isOtherwiseClock));
        }
        public UV GetEndPointUV(double startAngle, double length, bool isOtherwiseClock)
        {
            double plusAngle = isOtherwiseClock ? ConvertLength2Angle(length) : -ConvertLength2Angle(length);
            return GetEndPointUV(startAngle, plusAngle);
        }

        public ArcInfo GetArcInfo(double firstInitAngle, double distance1, double plusLength, bool isOtherwiseClock)
        {
            double startAngle = firstInitAngle + (isOtherwiseClock ? ConvertLength2Angle(distance1) : -ConvertLength2Angle(distance1));
            return GetArcInfo(startAngle, plusLength, isOtherwiseClock);
        }
        public void CalculateDistancesList(double targetLength, bool isOtherwiseClock)
        {
            double trueLength = targetLength - SingleWPF.Instance.SelectedBarDiameter * SingleWPF.Instance.DevelopMultiply;
            double num = Perimeter / trueLength;
            if (GeomUtil.IsSmallerOrEqual(num, 1))
            {
                double angle1 = (ConvertLength2Angle(SingleWPF.Instance.SelectedBarDiameter) * SingleWPF.Instance.DevelopMultiply);
                Singleton.Instance.ArcInfos.Add(GetArcInfo2(Math.PI, -Math.PI));
                Singleton.Instance.ArcInfos.Last().AddArc(GetArcInfo2(0, -(Math.PI + angle1)));
                //StandardArcs.Add(new List<Arc> { GetArc(Math.PI, -Math.PI), GetArc(0, -(Math.PI + angle1)) });
                return;
            }
            double num2 = Perimeter / targetLength;
            if (GeomUtil.IsSmallerOrEqual(num2, 1))
            {
                double angle1 = (ConvertLength2Angle(targetLength) - Math.PI * 2) / 2;
                Singleton.Instance.ArcInfos.Add(GetArcInfo2(Math.PI + angle1, -Math.PI));
                Singleton.Instance.ArcInfos.Last().AddArc(GetArcInfo2(angle1, -(Math.PI + angle1 * 2)));
                //StandardArcs.Add(new List<Arc> { GetArc(Math.PI + angle1, -Math.PI), GetArc(angle1, -(Math.PI + angle1*2))});
                double angle2 = (ConvertLength2Angle(SingleWPF.Instance.SelectedBarDiameter * SingleWPF.Instance.DevelopMultiply));
                Singleton.Instance.ArcInfos.Add(GetArcInfo2(Math.PI + angle2, -angle2 * 2));
                //StandardArcs.Add(new List<Arc> { GetArc(Math.PI + angle2, -angle2 * 2) });
                return;
            }

            num = Math.Floor(num);
            for (int i = 0; i < num; i++)
            {
                Singleton.Instance.ArcInfos.Add(GetArcInfo(Math.PI, i * trueLength, targetLength, isOtherwiseClock));
                //StandardArcs.Add(new List<Arc> { GetArc(Math.PI, i * trueLength, targetLength, isOtherwiseClock) });
            }
            Singleton.Instance.ArcInfos.Add(GetArcInfo(Math.PI, num * trueLength, perimeter - num * trueLength + SingleWPF.Instance.SelectedBarDiameter * SingleWPF.Instance.DevelopMultiply, isOtherwiseClock));
            //StandardArcs.Add(new List<Arc> { GetArc(Math.PI, num * trueLength, perimeter - num * trueLength + SingleWPF.Instance.SelectedBarDiameter * SingleWPF.Instance.DevelopMultiply, isOtherwiseClock) });
        }
        public void CalculateNumberWithAngle(double spac, double sumAngle, bool isOtherwiseClock)
        {
            Angle = ConvertLength2Angle(spac);
            double num = sumAngle / Angle, rNum = Math.Round(num, 0);
            if (sumAngle == Math.PI * 2)
            {
                if (GeomUtil.IsSmaller(num, rNum +0.5))
                {
                    Number = (int)rNum;
                }
                else
                {
                    Number = (int)rNum + 1;
                }
            }
            else
            {
                Number = (int)rNum + 1;
            }
            Angle = isOtherwiseClock ? Angle : -Angle;
        }
    }
}
